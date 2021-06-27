using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;

namespace UmfrageWebApi.Brokers.Storage
{
    public partial interface IStorageBroker
    {
        public ValueTask<IQueryable<Person>> SelectAllePersonenAsync();
        public ValueTask<Person> SelectPersonFromIdAsync(int idPersonArt);
        public ValueTask<Person> InsertPersonAsync(Person person);
        public ValueTask<Person> DeletePersonAsync(Person person);
        public ValueTask<Person> UpdatePersonAsync(Person person);
    }
}
