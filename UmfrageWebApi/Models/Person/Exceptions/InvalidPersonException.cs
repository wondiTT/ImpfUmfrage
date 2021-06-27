using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person.Exceptions
{
    public class InvalidPersonException : Exception
    {
        public InvalidPersonException()
            : base("Invalide informationen: Stammdaten wurden nicht angegeben ") { }

        public InvalidPersonException(string parameterName, object parameterValue)
            : base($"Invalide Person, " +
                  $"ParameterName: {parameterName}, " +
                  $"ParameterValue: {parameterValue}.")
        { }
    }
}
