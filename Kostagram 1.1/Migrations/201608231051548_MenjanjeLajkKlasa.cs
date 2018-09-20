namespace Kostagram_1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenjanjeLajkKlasa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KomentarLajk", "LajkId", "dbo.Lajk");
            DropForeignKey("dbo.SlikaLajk", "LajkId", "dbo.Lajk");
            DropIndex("dbo.KomentarLajk", new[] { "LajkId" });
            DropIndex("dbo.SlikaLajk", new[] { "LajkId" });
            AddColumn("dbo.KomentarLajk", "ImeKorisnika", c => c.String());
            AddColumn("dbo.SlikaLajk", "ImeKorisnika", c => c.String());
            DropColumn("dbo.KomentarLajk", "LajkId");
            DropColumn("dbo.SlikaLajk", "LajkId");
            DropTable("dbo.Lajk");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Lajk",
                c => new
                    {
                        LajkId = c.Int(nullable: false, identity: true),
                        ImeKorisnika = c.String(),
                    })
                .PrimaryKey(t => t.LajkId);
            
            AddColumn("dbo.SlikaLajk", "LajkId", c => c.Int(nullable: false));
            AddColumn("dbo.KomentarLajk", "LajkId", c => c.Int(nullable: false));
            DropColumn("dbo.SlikaLajk", "ImeKorisnika");
            DropColumn("dbo.KomentarLajk", "ImeKorisnika");
            CreateIndex("dbo.SlikaLajk", "LajkId");
            CreateIndex("dbo.KomentarLajk", "LajkId");
            AddForeignKey("dbo.SlikaLajk", "LajkId", "dbo.Lajk", "LajkId", cascadeDelete: true);
            AddForeignKey("dbo.KomentarLajk", "LajkId", "dbo.Lajk", "LajkId", cascadeDelete: true);
        }
    }
}
