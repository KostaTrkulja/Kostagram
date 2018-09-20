using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("Slika")]
    public class Slika
    {

        public int SlikaId { get; set; }

        public Slika()
        {
            Komentari = new HashSet<Komentar>();
            TagRelacije = new HashSet<TagRelacija>();
            Lajkovi = new HashSet<SlikaLajk>();
        }

        public string ImeKorisnika { get; set; }


        public byte[] FajlSlike { get; set; }

        public string TipFajla { get; set; }

        public string Opis { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumKreiranja { get; set; }


        public virtual ICollection<TagRelacija> TagRelacije { get; set; }

        public virtual ICollection<Komentar> Komentari { get; set; }

        public virtual ICollection<SlikaLajk> Lajkovi { get; set; }

    }
}