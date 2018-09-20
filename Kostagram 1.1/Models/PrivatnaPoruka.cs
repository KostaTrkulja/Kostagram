using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("PrivatnaPoruka")]
    public class PrivatnaPoruka
    {
        public int PrivatnaPorukaId { get; set; }

        public string ImePosiljaoca { get; set; }

        public string ImePrimaoca { get; set; }

        public string Naslov { get; set; }

        public string Sadrzaj { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public bool Poslata { get; set; }

        public bool Procitana { get; set; }


    }
}