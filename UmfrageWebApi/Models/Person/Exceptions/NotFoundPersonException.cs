using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class NotFoundPersonException : Exception    
    {
        public NotFoundPersonException(int idPerson)
            :base($"Eine Person mit der Id: {idPerson} nicht gefunden.") { }
    }
}
