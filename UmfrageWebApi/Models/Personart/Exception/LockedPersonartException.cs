using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class LockedPersonartException : Exception
    {
        public LockedPersonartException(Exception innerException)
            :base("Diese Personart ist gesperrt, versuchen es nochmal später", innerException) { }
    }
}
