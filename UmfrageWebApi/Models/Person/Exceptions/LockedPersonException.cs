using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class LockedPersonException : Exception
    {
        public LockedPersonException(Exception innerException)
            :base("Diese Person ist gesperrt, versuchen es nochmal später", innerException) { }
    }
}
