using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class PersonartValidationException : Exception
    {
        public PersonartValidationException(Exception innerException)
            :base("Invalide Einträge, kontaktieren Sie den Support", innerException) { }
    }
}
