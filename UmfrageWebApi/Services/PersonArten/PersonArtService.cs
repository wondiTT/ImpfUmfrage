
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

        public ValueTask<List<PersonArt>> AllePersonArtenAbrufenAsync() =>
            TryCatch(async () =>
            {
                List<PersonArt> personartenDb = await this.storageBroker.SelectAllPersonArtAsync();

                return personartenDb;
            });

        public ValueTask<PersonArt> PersonArtAbrufenFromIdAsync(int idPersonart) =>
        TryCatch(async () =>
        {
            ValidateIdPersonart(idPersonart);
            PersonArt personartDb = await this.storageBroker.SelectPersonArtFromIdAsync(idPersonart);
            ValidateStoragePersonart(personartDb, idPersonart);

            return personartDb;
        });

        public ValueTask<PersonArt> PersonArtAendernAsync(PersonArt personart) =>
        TryCatch(async () =>
        {
            CheckEingabePersonartOnCreateOnModify(personart);
            PersonArt personartDb = await this.storageBroker.SelectPersonArtFromIdAsync(personart.PersonArtId);
            ValidateStoragePersonart(personartDb, personart.PersonArtId);
            ValidateAginstStoragePersonartOnModify(personart, personartDb);

            return await this.storageBroker.UpdatePersonArtAsync(personart);
        });

        public ValueTask<PersonArt> PersonArtErzeugenAsync(PersonArt personart) =>
        TryCatch(async () =>
        {
            CheckEingabePersonartOnCreateOnModify(personart);

            return await this.storageBroker.InsertPersonArtAsync(personart);
        });

        public ValueTask<bool> PersonArtLoeschenAsync(int idPersonart) =>
            TryCatch(async () =>
            {
                ValidateIdPersonart(idPersonart);
                PersonArt personartDb = await this.storageBroker.SelectPersonArtFromIdAsync(idPersonart);
                ValidateStoragePersonart(personartDb, idPersonart);

                PersonArt deletedPersonartDb = await this.storageBroker.DeletePersonArtAsync(personartDb);
                if (deletedPersonartDb != null) { return true; }
                else { return false; }
            });
    }
}
