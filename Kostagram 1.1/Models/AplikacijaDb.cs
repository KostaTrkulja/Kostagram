using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kostagram_1._1.Models
{
    public class AplikacijaDb : DbContext
    {
        public virtual DbSet<Slika> Slike { get; set; }
        public virtual DbSet<Komentar> Komentari { get; set; }
        public virtual DbSet<PrivatnaPoruka> PrivatnePoruke { get; set; }
        public virtual DbSet<Tag> Tagovi { get; set; }       
        public virtual DbSet<TagRelacija> TagRelacije { get; set; }
        public virtual DbSet<SlikaLajk> SlikaLajkovi { get; set; }
        public virtual DbSet<KomentarLajk> KomentarLajkovi { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Slika>()
        //        .HasOptional(a => a.Komentari)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Slika>()
        //        .HasOptional(s => s.TagRelacije)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Slika>()
        //        .HasOptional(a => a.Lajkovi)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Komentar>()
        //        .HasOptional(k => k.Lajkovi)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);

        //}
    }
}