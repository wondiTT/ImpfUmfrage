using System;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class AlreadyExistsPersonartException : Exception
    {
        public AlreadyExistsPersonartException(Exception innerException)
            :base("Eine Personart mit denselben Eigenschaften existiert bereits", innerException){ }
        
    }
}
