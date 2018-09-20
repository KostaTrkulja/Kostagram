using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("SlikaLajk")]
    public class SlikaLajk
    {
        public int SlikaLajkId { get; set; }

        public int SlikaId { get; set; }

        public string ImeKorisnika { get; set; }

        [ForeignKey("SlikaId")]
        public virtual Slika Slika { get; set; }
    }
}