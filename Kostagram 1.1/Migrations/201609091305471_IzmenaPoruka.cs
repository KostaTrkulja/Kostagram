namespace Kostagram_1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IzmenaPoruka : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrivatnaPoruka", "Poslata", c => c.Boolean(nullable: false));
            AddColumn("dbo.PrivatnaPoruka", "Procitana", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivatnaPoruka", "Procitana");
            DropColumn("dbo.PrivatnaPoruka", "Poslata");
        }
    }
}
