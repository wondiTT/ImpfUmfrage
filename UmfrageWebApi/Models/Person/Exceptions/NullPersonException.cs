using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class NullPersonException : Exception
    {
        public NullPersonException()
            :base("Die Person ist null.") { }
    }
}
