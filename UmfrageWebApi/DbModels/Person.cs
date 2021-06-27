using System;
using System.Collections.Generic;

#nullable disable

namespace UmfrageWebApi.DbModels
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string Strasse { get; set; }
        public int? Hausnummer { get; set; }
        public string Ausweisnummer { get; set; }
        public bool Geimpft { get; set; }
        public int PersonArtId { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime LetzteAenderung { get; set; }

        public virtual PersonArt PersonArt { get; set; }
    }
}
