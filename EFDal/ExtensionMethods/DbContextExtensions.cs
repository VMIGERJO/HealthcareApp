using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods
{
    public static class DbContextExtensions
    {
        public static TEntity ToTracked<TEntity>(this DbContext context, TEntity stubEntity) where TEntity : BaseEntity
        {
            // Check if there is already an entity tracked with the ID of the stub
            TEntity? trackedEntity = context.Set<TEntity>().Local.FirstOrDefault(e => e.Id == stubEntity.Id);
            if (trackedEntity == null)
            {
                // Begin tracking the stub if there is currently no corresponding entity being tracked
                trackedEntity = stubEntity;
                context.Entry(trackedEntity).State = EntityState.Unchanged;
            }

            return trackedEntity;
        }
    }

}
