using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UmfrageWebApi.DbModels;

namespace UmfrageWebApi.Services.PersonArten

{
    public interface IPersonArtService
    {
        public ValueTask<IQueryable<PersonArt>> AllePersonArtenAbrufenAsync();
        public ValueTask<PersonArt> PersonArtAbrufenFromIdAsync(int idPersonArt);
        public ValueTask<PersonArt> PersonArtErzeugenAsync(Person person);
        public ValueTask<bool> PersonArtLoeschenAsync(int idPersonArt);
        public ValueTask<PersonArt> PersonArtAendernAsync(PersonArt personart);
    }
}
