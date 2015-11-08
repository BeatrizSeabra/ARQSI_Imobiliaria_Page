namespace Imobiliaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permutas2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TipoImovels", "PaiID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoImovels", "PaiID", c => c.Int());
        }
    }
}
