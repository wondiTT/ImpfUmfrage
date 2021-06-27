using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class PersonartServiceException : Exception
    {
        public PersonartServiceException(Exception innerException)
            : base("Dienstfehler aufgetreten, kontaktiren Sie den Support.", innerException) { }
    }
}
