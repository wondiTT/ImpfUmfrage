using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person;
using UmfrageWebApi.Models.Person.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.Models.Personart;
using UmfrageWebApi.Models.Personart.Exceptions;

namespace UmfrageWebApi.Services.PersonArten
{
    public partial class PersonArtService
    {

        private bool IsDateNotRecent(DateTimeOffset dateTime)
        {
            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTime();
            int oneMinute = 1;
            TimeSpan difference = now.Subtract(dateTime);

            return Math.Abs(difference.TotalMinutes) > oneMinute;
        }

        private static void ValidateStoragePersonart(PersonArt storagePersonart, int idPersonart)
        {
            if (storagePersonart == null)
            {
                throw new NotFoundPersonException(idPersonart);
            }
        }

        private static void ValidatePersonart(PersonArtModel personart)
        {
            if (personart is null)
            {
                throw new NullPersonException();
            }
        }

        private static void CheckEingabePersonartOnCreateOnModify(PersonArt personart)
        {

        }

        public void ValidateAginstStoragePersonartOnModify(PersonArt inputPersonart, PersonArt storagePersonart)
        {

        }

        private static void ValidateIdPersonart(int idPersonart)
        {
            if (!(idPersonart > 0)) { throw new InvalidPersonartException(); }
        }
        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        private static bool IsInvalid(DateTimeOffset date) => date == default;
    }
}
