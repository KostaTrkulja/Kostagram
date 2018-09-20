namespace Kostagram_1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KaskadePriBrisanju : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KomentarLajk", "KomentarId", "dbo.Komentar");
            DropForeignKey("dbo.Komentar", "SlikaId", "dbo.Slika");
            DropForeignKey("dbo.SlikaLajk", "SlikaId", "dbo.Slika");
            DropForeignKey("dbo.TagRelacija", "SlikaId", "dbo.Slika");
            AddColumn("dbo.Komentar", "Lajkovi_KomentarLajkId", c => c.Int());
            AddColumn("dbo.Slika", "Komentari_KomentarId", c => c.Int());
            AddColumn("dbo.Slika", "Lajkovi_SlikaLajkId", c => c.Int());
            AddColumn("dbo.Slika", "TagRelacije_TagRelacijaId", c => c.Int());
            CreateIndex("dbo.Komentar", "Lajkovi_KomentarLajkId");
            CreateIndex("dbo.Slika", "Komentari_KomentarId");
            CreateIndex("dbo.Slika", "Lajkovi_SlikaLajkId");
            CreateIndex("dbo.Slika", "TagRelacije_TagRelacijaId");
            AddForeignKey("dbo.Komentar", "Lajkovi_KomentarLajkId", "dbo.KomentarLajk", "KomentarLajkId", cascadeDelete: true);
            AddForeignKey("dbo.Slika", "Komentari_KomentarId", "dbo.Komentar", "KomentarId", cascadeDelete: true);
            AddForeignKey("dbo.Slika", "Lajkovi_SlikaLajkId", "dbo.SlikaLajk", "SlikaLajkId", cascadeDelete: true);
            AddForeignKey("dbo.Slika", "TagRelacije_TagRelacijaId", "dbo.TagRelacija", "TagRelacijaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slika", "TagRelacije_TagRelacijaId", "dbo.TagRelacija");
            DropForeignKey("dbo.Slika", "Lajkovi_SlikaLajkId", "dbo.SlikaLajk");
            DropForeignKey("dbo.Slika", "Komentari_KomentarId", "dbo.Komentar");
            DropForeignKey("dbo.Komentar", "Lajkovi_KomentarLajkId", "dbo.KomentarLajk");
            DropIndex("dbo.Slika", new[] { "TagRelacije_TagRelacijaId" });
            DropIndex("dbo.Slika", new[] { "Lajkovi_SlikaLajkId" });
            DropIndex("dbo.Slika", new[] { "Komentari_KomentarId" });
            DropIndex("dbo.Komentar", new[] { "Lajkovi_KomentarLajkId" });
            DropColumn("dbo.Slika", "TagRelacije_TagRelacijaId");
            DropColumn("dbo.Slika", "Lajkovi_SlikaLajkId");
            DropColumn("dbo.Slika", "Komentari_KomentarId");
            DropColumn("dbo.Komentar", "Lajkovi_KomentarLajkId");
            AddForeignKey("dbo.TagRelacija", "SlikaId", "dbo.Slika", "SlikaId", cascadeDelete: true);
            AddForeignKey("dbo.SlikaLajk", "SlikaId", "dbo.Slika", "SlikaId", cascadeDelete: true);
            AddForeignKey("dbo.Komentar", "SlikaId", "dbo.Slika", "SlikaId", cascadeDelete: true);
            AddForeignKey("dbo.KomentarLajk", "KomentarId", "dbo.Komentar", "KomentarId", cascadeDelete: true);
        }
    }
}
