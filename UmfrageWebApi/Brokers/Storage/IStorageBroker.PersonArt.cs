using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;

namespace UmfrageWebApi.Brokers.Storage
{
    public partial interface IStorageBroker
    {
        public ValueTask<List<PersonArt>> SelectAllPersonArtAsync();
        public ValueTask<PersonArt> SelectPersonArtFromIdAsync(int idPersonArt);
        public ValueTask<PersonArt> InsertPersonArtAsync(PersonArt personArt);
        public ValueTask<PersonArt> DeletePersonArtAsync(PersonArt personArt);
        public ValueTask<PersonArt> UpdatePersonArtAsync(PersonArt personArt);

    }
}
