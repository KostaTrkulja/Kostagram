using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("Komentar")]
    public class Komentar
    {
        public int KomentarId { get; set; }

        public int SlikaId { get; set; }

        public Komentar()
        {
            Lajkovi = new HashSet<KomentarLajk>();
        }

        public string TeloKomentara { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public string ImeKorisnika { get; set; }

        [ForeignKey("SlikaId")]
        public virtual Slika Slika { get; set; }

        public virtual ICollection<KomentarLajk> Lajkovi { get; set; }
    }
}