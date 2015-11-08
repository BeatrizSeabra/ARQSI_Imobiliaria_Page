namespace Imobiliaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoImovels",
                c => new
                    {
                        TipoImovelID = c.Int(nullable: false, identity: true),
                        NomeTipo = c.String(),
                        TipoImovel_TipoImovelID = c.Int(),
                    })
                .PrimaryKey(t => t.TipoImovelID)
                .ForeignKey("dbo.TipoImovels", t => t.TipoImovel_TipoImovelID)
                .Index(t => t.TipoImovel_TipoImovelID);
            
            CreateTable(
                "dbo.Utilizadors",
                c => new
                    {
                        UtilizadorID = c.String(nullable: false, maxLength: 128),
                        PrimeiroNome = c.String(),
                        UltimoNome = c.String(),
                    })
                .PrimaryKey(t => t.UtilizadorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoImovels", "TipoImovel_TipoImovelID", "dbo.TipoImovels");
            DropIndex("dbo.TipoImovels", new[] { "TipoImovel_TipoImovelID" });
            DropTable("dbo.Utilizadors");
            DropTable("dbo.TipoImovels");
        }
    }
}
