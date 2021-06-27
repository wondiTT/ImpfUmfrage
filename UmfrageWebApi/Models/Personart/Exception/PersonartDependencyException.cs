using System;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class PersonartDependencyException : Exception
    {
        public PersonartDependencyException(Exception innerException)
            : base("Dienstabhängigkeitsfehler aufgetreten, kontaktieren Sie den Support", innerException) { }
    }
}
