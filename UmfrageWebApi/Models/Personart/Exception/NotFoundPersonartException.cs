using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class NotFoundPersonartException : Exception    
    {
        public NotFoundPersonartException(int idPerson)
            :base($"Personart mit der Id: {idPerson} nicht gefunden.") { }
    }
}
