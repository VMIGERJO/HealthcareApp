using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class NoResultsFoundException : Exception
    {
        public NoResultsFoundException() : base("No results were found.") { }

    }
}
