using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class NotFoundPersonartException : Exception    
    {
        public NotFoundPersonartException(int idPersonart)
            :base($"Personart mit der Id: {idPersonart} nicht gefunden.") { }
    }
}
