using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class PersonServiceException : Exception
    {
        public PersonServiceException(Exception innerException)
            : base("Dienstfehler aufgetreten, kontaktiren Sie den Support.", innerException) { }
    }
}
