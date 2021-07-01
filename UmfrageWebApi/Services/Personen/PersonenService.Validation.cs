using UmfrageWebApi.DbModels;
using UmfrageWebApi.Models.Person;
using UmfrageWebApi.Models.Person.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Services.Personen
{
    public partial class PersonenService
    {

        private bool IsDateNotRecent(DateTimeOffset dateTime)
        {
            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTime();
            int oneMinute = 1;
            TimeSpan difference = now.Subtract(dateTime);

            return Math.Abs(difference.TotalMinutes) > oneMinute;
        }

        private static void ValidateStoragePerson(Person storagePerson, int idPerson)
        {
            if (storagePerson == null)
            {
                throw new NotFoundPersonException(idPerson);
            }
        }

        private static void ValidatePerson(PersonModel person)
        {
            if (person is null)
            {
                throw new NullPersonException();
            }
        }

        private static void CheckEingabePersonOnCreateOnModify(Person person)
        {
            if (IsInvalid(person.Nachname) || IsInvalid(person.Vorname) || IsInvalid(person.Ausweisnummer) || IsInvalid(person.Geburtsdatum) || IsInvalidId(person.PersonArtId) || IsInvalidBit(person.Geimpft))
            {
                throw new InvalidPersonException();
            }
        }

        public void ValidateAginstStoragePersonOnModify(Person inputPerson, Person storagePerson)
        {
           
        }

        private static void ValidateIdPerson(int idPerson)
        {
            if (!(idPerson > 0)){ throw new InvalidPersonException(); }
        }
        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        private static bool IsInvalid(DateTimeOffset date) => date == default;
        private static bool IsInvalidId(int input) => input > 0;
        private static bool IsInvalidBit(bool input) => input is true || input is false;
    }
}
