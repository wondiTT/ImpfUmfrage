using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Person
{
    public class PersonModelForCreation
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string Strasse { get; set; }
        public int? Hausnummer { get; set; }
        public string Ausweisnummer { get; set; }
        public bool Geimpft { get; set; }
        public int PersonArtId { get; set; }
    }
}
