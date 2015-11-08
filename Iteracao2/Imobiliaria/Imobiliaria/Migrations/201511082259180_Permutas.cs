namespace Imobiliaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permutas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoImovels", "PaiID", c => c.Int());
            AddColumn("dbo.Anuncios", "TiposImovelID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anuncios", "TiposImovelID");
            DropColumn("dbo.TipoImovels", "PaiID");
        }
    }
}
