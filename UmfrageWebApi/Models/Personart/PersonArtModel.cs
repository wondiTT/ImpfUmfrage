using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UmfrageWebApi.Models.Personart
{
    public class PersonArtModel
    {
        public int PersonArtId { get; set; }
        public string Beschreibung { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
