using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class PersonValidationException : Exception
    {
        public PersonValidationException(Exception innerException)
            :base("Invalide Einträge, kontaktieren Sie den Support", innerException) { }
    }
}
