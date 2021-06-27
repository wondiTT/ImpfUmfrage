using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;

namespace UmfrageWebApi.Brokers.Storage
{
    public partial class StorageBroker
    {
        public async ValueTask<IQueryable<Person>> SelectAllePersonenAsync()
        {
            var query = Personen.Include(p => p.PersonArt).AsNoTracking().AsQueryable();

            return await Task.FromResult(query);
        }
        public async ValueTask<Person> SelectPersonFromIdAsync(int idPerson)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var person = await Personen.FirstOrDefaultAsync(p => p.PersonId == idPerson);

            return person;
        }
        public async ValueTask<Person> InsertPersonAsync(Person person)
        {
            EntityEntry<Person> personEntityEntry = await Personen.AddAsync(person);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
        public async ValueTask<Person> UpdatePersonAsync(Person person)
        {
            EntityEntry<Person> personEntityEntry = Personen.Update(person);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
        public async ValueTask<Person> DeletePersonAsync(Person person)
        {
            EntityEntry<Person> personEntityEntry = Personen.Remove(person);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
    }
}
