using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Exceptions
{
    public class NonUniqueQueryException : Exception
    {
        public NonUniqueQueryException() : base("Too many results found, please be more specific.") { }
    }
}
