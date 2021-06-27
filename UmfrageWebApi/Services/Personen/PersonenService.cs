
using UmfrageWebApi.Brokers.DateTimes;
using UmfrageWebApi.Brokers.Storage;
using UmfrageWebApi.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Services.Personen
{
    public partial class PersonenService : IPersonenService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public PersonenService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<IQueryable<Person>> AllePersonenAbrufenAsync() =>
            TryCatch(async () =>
            {
                IQueryable<Person> personenDb = await this.storageBroker.SelectAllePersonenAsync();

                return personenDb;
            });

        public ValueTask<Person> PersonAbrufenFromIdAsync(int idPerson) =>
        TryCatch(async () =>
        {
            ValidateIdPerson(idPerson);
            Person personDb = await this.storageBroker.SelectPersonFromIdAsync(idPerson);
            ValidateStoragePerson(personDb, idPerson);

            return personDb;
        });

        public ValueTask<Person> PersonAendernAsync(Person person) =>
        TryCatch(async () =>
        {
            CheckEingabePersonOnCreateOnModify(person);
            Person personDb = await this.storageBroker.SelectPersonFromIdAsync(person.PersonId);
            ValidateStoragePerson(personDb, person.PersonId);
            ValidateAginstStoragePersonOnModify(person, personDb);

            return await this.storageBroker.UpdatePersonAsync(person);
        });

        public ValueTask<Person> PersonErzeugenAsync(Person person) =>
        TryCatch(async () =>
        {
            CheckEingabePersonOnCreateOnModify(person);

            return await this.storageBroker.InsertPersonAsync(person);
        });

        public ValueTask<bool> PersonLoeschenAsync(int idPerson) =>
            TryCatch(async () =>
            {
                ValidateIdPerson(idPerson);
                Person personDb = await this.storageBroker.SelectPersonFromIdAsync(idPerson);
                ValidateStoragePerson(personDb, idPerson);

                Person deletedPersonDb = await this.storageBroker.DeletePersonAsync(personDb);
                if(deletedPersonDb != null) { return true; }
                else { return false; }
            });
    }
}
