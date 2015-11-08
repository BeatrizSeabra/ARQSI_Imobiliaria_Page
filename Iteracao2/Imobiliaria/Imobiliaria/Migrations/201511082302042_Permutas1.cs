namespace Imobiliaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permutas1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Anuncios", "TiposImovelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Anuncios", "TiposImovelID", c => c.Int());
        }
    }
}
