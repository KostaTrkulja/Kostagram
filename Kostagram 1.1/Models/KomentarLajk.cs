using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("KomentarLajk")]
    public class KomentarLajk
    {
        public int KomentarLajkId { get; set; }

        public int KomentarId { get; set; }

        public string ImeKorisnika { get; set; }

        [ForeignKey("KomentarId")]
        public virtual Komentar Komentar { get; set; }

    }
}