using DAL.DapperAttributes;
using DAL.Entities;

using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DAL.Repositories.DapperRepositories
{
    public class DapperGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        internal readonly DbConnectionFactory _dbConnectionFactory;

        public DapperGenericRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<TEntity>> SearchAsync(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc = false, params Expression<Func<TEntity, object>>[] includes)
        {
            string tableName = GetTableName().ToLower();
            StringBuilder query = new StringBuilder($"SELECT * FROM {tableName} ");

            DynamicParameters parameters = new DynamicParameters();
            if (includes.Any())
            {
                IncludeRelatedEntities(includes, query, tableName);
            }
            else
            {
                query.Append("WHERE 1 = 1");
            }

            ApplyFilters(filters, query, parameters);
            ApplyOrder(orderExpression, orderAsc, query, tableName);

            return await ExecuteQueryListAsync(query.ToString(), parameters);
        }

        protected void ApplyOrder<TEntity>(Expression<Func<TEntity, object>> orderExpression, bool orderAsc, StringBuilder query, string tableName)
        {
            var columnName = GetColumnName(orderExpression);
            if (orderAsc)
            {
                query.Append(orderAsc ? $" ORDER BY {columnName} ASC" : $" ORDER BY {columnName}");
            }
            else
            {
                query.Append(orderAsc ? $" ORDER BY {columnName} DESC" : $" ORDER BY {columnName}");
            }
            
        }

        protected async Task<List<TEntity>> ExecuteQueryListAsync(string query, DynamicParameters parameters)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<TEntity>(query, parameters);
                return results.ToList();
            }
        }

        protected string GetTableName()
        {
            if (typeof(TEntity) == typeof(Address))
            {
                return typeof(TEntity).Name;
            }
            return typeof(TEntity).Name.ToLower() + "s";
        }

        protected string GetTableName(Type entityType)
        {
            if (entityType == typeof(Address))
            {
                return entityType.Name;
            }
            return entityType.Name.ToLower() + "s";
        }

        protected string GetColumnName(Expression expression)
        {
            switch (expression)
            {
                case LambdaExpression lambdaExpression when lambdaExpression.Body is BinaryExpression lambdaBinaryExpression:
                    // Handle BinaryExpression (e.g., ==)
                    var binaryMemberExpression = lambdaBinaryExpression.Left as MemberExpression;
                    return binaryMemberExpression?.Member.Name.ToLower() ?? string.Empty;

                case LambdaExpression lambdaExpression when lambdaExpression.Body is MethodCallExpression lambdaMethodCallExpression:
                    // Handle MethodCallExpression (e.g., .Contains)
                    if (lambdaMethodCallExpression.Object is MemberExpression propertyExpression)
                    {
                        return propertyExpression?.Member.Name.ToLower() ?? string.Empty;
                    }
                    return string.Empty;

                case LambdaExpression lambdaExpression when lambdaExpression.Body is MemberExpression memberExpression:
                    return memberExpression?.Member.Name.ToLower() ?? string.Empty;

                case LambdaExpression lambdaExpression when lambdaExpression.Body is UnaryExpression unaryExpression:
                    {
                        MemberExpression operandExpression = unaryExpression.Operand as MemberExpression;
                        return operandExpression?.Member.Name.ToLower() ?? string.Empty;
                    }

                default:
                    throw new ArgumentException("Invalid expression type for GetColumnName");
            }
        }


        protected DynamicParameters GetParameters<TEntity>(List<Expression<Func<TEntity, bool>>> filters)
        {
            var parameters = new DynamicParameters();

            foreach (var filter in filters)
            {
                var binaryExpression = filter.Body as BinaryExpression;
                if (binaryExpression != null && binaryExpression.Right is ConstantExpression constantExpression)
                {
                    parameters.Add($"@{GetColumnName(filter)}", constantExpression.Value);
                }
            }

            return parameters;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var tableName = GetTableName().ToLower();
            var query = $"SELECT * FROM {tableName}";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return (await connection.QueryAsync<TEntity>(query)).ToList();
            }

        }


        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            string tableName = GetTableName().ToLower();
            StringBuilder query = new StringBuilder($"SELECT * FROM {tableName} WHERE Id = @Id");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Include related entities
            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    var joinTableName = GetTableName(include.ReturnType).ToLower();
                    var joinColumnName = include.Body.ToString().Split('.')[1];

                    // Perform LEFT JOIN on related entity
                    query.Append($" LEFT JOIN {joinTableName} ON {tableName}.{joinColumnName} = {joinTableName}.Id");
                }
            }

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<TEntity>(query.ToString(), parameters);
                return result;
            }
        }

        public virtual int Insert(TEntity entity)
        {
            string tableName = GetTableName().ToLower();
            IEnumerable<string> propertyNames = GetPropertyNames(entity);

            string query = $"INSERT INTO {tableName} ({string.Join(", ", propertyNames)}) VALUES ({string.Join(", ", propertyNames.Select(p => "@" + p))}) SELECT SCOPE_IDENTITY()";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return connection.QuerySingle<int>(query, entity);
            }
        }

        protected IEnumerable<string> GetNavigationPropertyNames()
        {
            var entityType = typeof(TEntity);

            var foreignKeyProperties = entityType.GetProperties()
                .Where(property =>
                    property.GetCustomAttribute<NavigationAttribute>() != null)
                .Select(property => property.Name);

            return foreignKeyProperties;
        }

        protected IEnumerable<string> GetPropertyNames(TEntity entity)
        {
            var propertyNames = typeof(TEntity).GetProperties().Select(property => property.Name);
            IEnumerable<string> navigationPropertyNames = GetNavigationPropertyNames();
            return propertyNames.Where(p => p != "Id" && !navigationPropertyNames.Contains(p)).ToList();
        }

        public void Update(TEntity entity)
        {
            var tableName = GetTableName().ToLower();
            var propertyNames = GetPropertyNames(entity);

            // Exclude the property named "Id" from the update
            var updateColumns = propertyNames.Where(p => p.ToLower() != "id");

            var query = $"UPDATE {tableName} SET {string.Join(", ", updateColumns.Select(p => $"{p} = @{p}"))} WHERE Id = @Id";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(int id)
        {
            var tableName = GetTableName().ToLower();
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Execute(query, new { Id = id });
            }
        }


        public async Task<TEntity> SearchUniqueAsync(List<Expression<Func<TEntity, bool>>> filters, params Expression<Func<TEntity, object>>[] includes)
        {
            var tableName = GetTableName().ToLower();
            var query = new StringBuilder($"SELECT * FROM {tableName} WHERE 1=1");
            var parameters = new DynamicParameters();

            ApplyFilters(filters, query, parameters);

            IncludeRelatedEntities(includes, query, tableName);

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<TEntity>(query.ToString(), parameters);

                return results.SingleOrDefault();
            }
        }


        protected void ApplyFilters(List<Expression<Func<TEntity, bool>>> filters, StringBuilder query, DynamicParameters parameters)
        {
            foreach (var filter in filters)
            {
                if (filter.Body is BinaryExpression binaryExpression)
                {
                    ApplyBinaryExpressionFilter(binaryExpression, filter, query, parameters);
                }
                else if (filter.Body is MethodCallExpression methodCallExpression &&
                         methodCallExpression.Method.Name == "Contains" &&
                         methodCallExpression.Object is MemberExpression propertyExpression &&
                         propertyExpression.Type == typeof(string))
                {
                    ApplyContainsFilter(methodCallExpression, filter, query, parameters);
                }
            }
        }

        protected void ApplyBinaryExpressionFilter(BinaryExpression binaryExpression, Expression<Func<TEntity, bool>> filter, StringBuilder query, DynamicParameters parameters)
        {
            // Handle BinaryExpression (e.g., ==)
            MemberExpression left = binaryExpression.Left as MemberExpression;
            MemberExpression right = binaryExpression.Right as MemberExpression;

            if (left != null)
            {
                var columnName = GetColumnName(filter);

                if (binaryExpression.NodeType == ExpressionType.Equal)
                {
                    if (right != null)
                    {
                        // Compile and invoke the right side expression to get its value
                        var rightValue = GetPropertyValue(right);

                        // Handle == for string properties on both sides
                        query.Append($" AND {columnName} = @{columnName}");
                        parameters.Add($"@{columnName}", rightValue);
                    }
                    else if (binaryExpression.Right is ConstantExpression constantExpression)
                    {
                        // Handle == for string properties with constant right side
                        query.Append($" AND {columnName} = @{columnName}");
                        parameters.Add($"@{columnName}", constantExpression.Value);
                    }
                }
            }
        }

        protected void ApplyContainsFilter(MethodCallExpression methodCallExpression, Expression<Func<TEntity, bool>> filter, StringBuilder query, DynamicParameters parameters)
        {
            var columnName = GetColumnName(filter);
            query.Append($" AND {columnName} LIKE @{columnName}");

            string argument = "";

            if (methodCallExpression.Arguments[0] is MemberExpression argumentExpression)
            {
                argument = GetPropertyValue(argumentExpression).ToString().ToLower();
            }
            else if (methodCallExpression.Arguments[0] is ConstantExpression constantExpression)
            {
                argument = constantExpression.Value.ToString().ToLower();
            }

            parameters.Add($"@{columnName}", $"%{argument.Trim('"')}%");
        }

        protected void IncludeRelatedEntities(Expression<Func<TEntity, object>>[] includes, StringBuilder query, string tableName)
        {
            foreach (var include in includes)
            {
                MemberExpression joinExpression = include.Body as MemberExpression;
                var joinTableName = GetTableName(joinExpression.Type).ToLower();
                var joinColumnName = include.Body.ToString().Split('.')[1] + "id";
                query.Append($" LEFT JOIN {joinTableName} ON {tableName}.{joinColumnName} = {joinTableName}.Id WHERE 1 = 1 ");
            }
        }

        protected async Task<TEntity> ExecuteQueryAsync(string query, DynamicParameters parameters)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<TEntity>(query, parameters);
                return result.First();
            }
        }
        protected object GetPropertyValue(MemberExpression memberExpression)
        {
            // Compile and invoke the expression to get the actual value
            var lambda = Expression.Lambda(memberExpression);
            var compiledLambda = lambda.Compile();
            return compiledLambda.DynamicInvoke();
        }

    }
}
