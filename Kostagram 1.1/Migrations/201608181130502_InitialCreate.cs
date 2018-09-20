namespace Kostagram_1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komentar",
                c => new
                    {
                        KomentarId = c.Int(nullable: false, identity: true),
                        SlikaId = c.Int(nullable: false),
                        TeloKomentara = c.String(),
                        DatumKreiranja = c.DateTime(nullable: false),
                        ImeKorisnika = c.String(),
                    })
                .PrimaryKey(t => t.KomentarId)
                .ForeignKey("dbo.Slika", t => t.SlikaId, cascadeDelete: true)
                .Index(t => t.SlikaId);
            
            CreateTable(
                "dbo.KomentarLajk",
                c => new
                    {
                        KomentarLajkId = c.Int(nullable: false, identity: true),
                        LajkId = c.Int(nullable: false),
                        KomentarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KomentarLajkId)
                .ForeignKey("dbo.Komentar", t => t.KomentarId, cascadeDelete: true)
                .ForeignKey("dbo.Lajk", t => t.LajkId, cascadeDelete: true)
                .Index(t => t.LajkId)
                .Index(t => t.KomentarId);
            
            CreateTable(
                "dbo.Lajk",
                c => new
                    {
                        LajkId = c.Int(nullable: false, identity: true),
                        ImeKorisnika = c.String(),
                    })
                .PrimaryKey(t => t.LajkId);
            
            CreateTable(
                "dbo.SlikaLajk",
                c => new
                    {
                        SlikaLajkId = c.Int(nullable: false, identity: true),
                        LajkId = c.Int(nullable: false),
                        SlikaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SlikaLajkId)
                .ForeignKey("dbo.Lajk", t => t.LajkId, cascadeDelete: true)
                .ForeignKey("dbo.Slika", t => t.SlikaId, cascadeDelete: true)
                .Index(t => t.LajkId)
                .Index(t => t.SlikaId);
            
            CreateTable(
                "dbo.Slika",
                c => new
                    {
                        SlikaId = c.Int(nullable: false, identity: true),
                        ImeKorisnika = c.String(),
                        FajlSlike = c.Binary(),
                        TipFajla = c.String(),
                        Opis = c.String(),
                        DatumKreiranja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SlikaId);
            
            CreateTable(
                "dbo.TagRelacija",
                c => new
                    {
                        TagRelacijaId = c.Int(nullable: false, identity: true),
                        SlikaId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagRelacijaId)
                .ForeignKey("dbo.Slika", t => t.SlikaId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.SlikaId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        SadrzajTaga = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.PrivatnaPoruka",
                c => new
                    {
                        PrivatnaPorukaId = c.Int(nullable: false, identity: true),
                        ImePosiljaoca = c.String(),
                        ImePrimaoca = c.String(),
                        Naslov = c.String(),
                        Sadrzaj = c.String(),
                        DatumKreiranja = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PrivatnaPorukaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagRelacija", "TagId", "dbo.Tag");
            DropForeignKey("dbo.TagRelacija", "SlikaId", "dbo.Slika");
            DropForeignKey("dbo.SlikaLajk", "SlikaId", "dbo.Slika");
            DropForeignKey("dbo.Komentar", "SlikaId", "dbo.Slika");
            DropForeignKey("dbo.SlikaLajk", "LajkId", "dbo.Lajk");
            DropForeignKey("dbo.KomentarLajk", "LajkId", "dbo.Lajk");
            DropForeignKey("dbo.KomentarLajk", "KomentarId", "dbo.Komentar");
            DropIndex("dbo.TagRelacija", new[] { "TagId" });
            DropIndex("dbo.TagRelacija", new[] { "SlikaId" });
            DropIndex("dbo.SlikaLajk", new[] { "SlikaId" });
            DropIndex("dbo.SlikaLajk", new[] { "LajkId" });
            DropIndex("dbo.KomentarLajk", new[] { "KomentarId" });
            DropIndex("dbo.KomentarLajk", new[] { "LajkId" });
            DropIndex("dbo.Komentar", new[] { "SlikaId" });
            DropTable("dbo.PrivatnaPoruka");
            DropTable("dbo.Tag");
            DropTable("dbo.TagRelacija");
            DropTable("dbo.Slika");
            DropTable("dbo.SlikaLajk");
            DropTable("dbo.Lajk");
            DropTable("dbo.KomentarLajk");
            DropTable("dbo.Komentar");
        }
    }
}
