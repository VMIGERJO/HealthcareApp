using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DapperAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class NavigationAttribute : Attribute
    {
        public NavigationAttribute()
        {
        }
    }
}
