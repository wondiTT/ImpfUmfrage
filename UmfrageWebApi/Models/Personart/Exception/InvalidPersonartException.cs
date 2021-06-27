using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart.Exceptions
{
    public class InvalidPersonartException : Exception
    {
        public InvalidPersonartException()
            : base("Invalide informationen: Beschreibung ist nicht gesetzt") { }

        public InvalidPersonartException(string parameterName, object parameterValue)
            : base($"Invalide Person, " +
                  $"ParameterName: {parameterName}, " +
                  $"ParameterValue: {parameterValue}.")
        { }
    }
}
