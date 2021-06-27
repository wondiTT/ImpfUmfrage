using System;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class PersonDependencyException : Exception
    {
        public PersonDependencyException(Exception innerException)
            : base("Dienstabhängigkeitsfehler aufgetreten, kontaktieren Sie den Support", innerException) { }
    }
}
