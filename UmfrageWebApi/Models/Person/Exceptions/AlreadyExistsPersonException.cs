using System;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class AlreadyExistsPersonException : Exception
    {
        public AlreadyExistsPersonException(Exception innerException)
            :base("Eine Person mit denselben Eigenschaften existiert bereits", innerException){ }
        
    }
}
