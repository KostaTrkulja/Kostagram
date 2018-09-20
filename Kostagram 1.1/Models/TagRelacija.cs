using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("TagRelacija")]
    public class TagRelacija
    {
        public int TagRelacijaId { get; set; }

        public int SlikaId { get; set; }
        public int TagId { get; set; }

        [ForeignKey("SlikaId")]
        public virtual Slika Slika { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}