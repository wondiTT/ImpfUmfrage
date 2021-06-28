
using UmfrageWebApi.Brokers.DateTimes;
using UmfrageWebApi.Brokers.Storage;
using UmfrageWebApi.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Services.PersonArten
{
    public partial class PersonArtService : IPersonArtService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public PersonArtService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<IQueryable<PersonArt>> AllePersonArtenAbrufenAsync() =>
            TryCatch(async () =>
            {
                IQueryable<PersonArt> personartenDb = await this.storageBroker.SelectAllPersonArtAsync();

                return personartenDb;
            });

        public ValueTask<PersonArt> PersonArtAbrufenFromIdAsync(int idPersonart) =>
        TryCatch(async () =>
        {
            ValidateIdPerson(idPersonart);
            Person personartDb = await this.storageBroker.SelectPersonArtFromIdAsync(idPersonart);
            ValidateStoragePerson(personDb, idPerson);

            return personDb;
        });

        public ValueTask<PersonArt> PersonArtAendernAsync(PersonArt personart) =>
        TryCatch(async () =>
        {
            CheckEingabePersonOnCreateOnModify(person);
            Person personDb = await this.storageBroker.SelectPersonFromIdAsync(person.PersonId);
            ValidateStoragePerson(personDb, person.PersonId);
            ValidateAginstStoragePersonOnModify(person, personDb);

            return await this.storageBroker.UpdatePersonAsync(person);
        });

        public ValueTask<PersonArt> PersonArtErzeugenAsync(PersonArt personart) =>
        TryCatch(async () =>
        {
            CheckEingabePersonOnCreateOnModify(person);

            return await this.storageBroker.InsertPersonAsync(person);
        });

        public ValueTask<bool> PersonArtLoeschenAsync(int idPerson) =>
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
