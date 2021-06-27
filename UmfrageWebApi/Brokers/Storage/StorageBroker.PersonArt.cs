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
        public async ValueTask<List<PersonArt>> SelectAllPersonArtAsync()
        {
            var query = PersonArten.AsNoTracking().ToList();

            return await Task.FromResult(query);
        }
        public async ValueTask<PersonArt> SelectPersonArtFromIdAsync(int personartId)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var person = await PersonArten.FirstOrDefaultAsync(p => p.PersonArtId == personartId);

            return person;
        }
        public async ValueTask<PersonArt> InsertPersonArtAsync(PersonArt personart)
        {
            EntityEntry<PersonArt> personEntityEntry = await PersonArten.AddAsync(personart);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
        public async ValueTask<PersonArt> UpdatePersonArtAsync(PersonArt personart)
        {
            EntityEntry<PersonArt> personEntityEntry = PersonArten.Update(personart);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
        public async ValueTask<PersonArt> DeletePersonArtAsync(PersonArt personart)
        {
            EntityEntry<PersonArt> personEntityEntry = PersonArten.Remove(personart);
            await this.SaveChangesAsync();

            return personEntityEntry.Entity;
        }
    }
}
