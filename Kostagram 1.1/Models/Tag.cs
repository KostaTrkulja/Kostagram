using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kostagram_1._1.Models
{
    [Table("Tag")]
    public class Tag
    {
        public int TagId { get; set; }

        public string SadrzajTaga { get; set; }

        public Tag()
        {
            TagRelacije = new HashSet<TagRelacija>();
        }

        public virtual ICollection<TagRelacija> TagRelacije { get; set; }
    }
}