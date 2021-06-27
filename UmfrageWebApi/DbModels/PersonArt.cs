using System;
using System.Collections.Generic;

#nullable disable

namespace UmfrageWebApi.DbModels
{
    public partial class PersonArt
    {
        public PersonArt()
        {
            Personen = new HashSet<Person>();
        }

        public int PersonArtId { get; set; }
        public string Beschreibung { get; set; }
        public DateTime LetzteAenderung { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Person> Personen { get; set; }
    }
}
