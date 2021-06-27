using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;

namespace UmfrageWebApi.Services.Personen
{
    public interface IPersonenService
    {
        public ValueTask<IQueryable<Person>> AllePersonenAbrufenAsync();
        public ValueTask<Person> PersonAbrufenFromIdAsync(int idPerson);
        public ValueTask<Person> PersonErzeugenAsync(Person person);
        public ValueTask<bool> PersonLoeschenAsync(int idPerson);
        public ValueTask<Person> PersonAendernAsync(Person person);
    }
}
