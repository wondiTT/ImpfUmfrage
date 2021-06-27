using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class NullPersonartException : Exception
    {
        public NullPersonartException()
            :base("Die Personart ist null.") { }
    }
}
