namespace Imobiliaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Classes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alertas",
                c => new
                    {
                        AlertaID = c.Int(nullable: false, identity: true),
                        Lido = c.Boolean(nullable: false),
                        Anuncio_AnuncioID = c.Int(),
                        Utilizador_UtilizadorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlertaID)
                .ForeignKey("dbo.Anuncios", t => t.Anuncio_AnuncioID)
                .ForeignKey("dbo.Utilizadors", t => t.Utilizador_UtilizadorID)
                .Index(t => t.Anuncio_AnuncioID)
                .Index(t => t.Utilizador_UtilizadorID);
            
            CreateTable(
                "dbo.Anuncios",
                c => new
                    {
                        AnuncioID = c.Int(nullable: false, identity: true),
                        Mediador = c.String(),
                        CriadoEm = c.DateTime(nullable: false),
                        Preco = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CriadoPor_UtilizadorID = c.String(maxLength: 128),
                        Imovel_ImovelID = c.Int(),
                    })
                .PrimaryKey(t => t.AnuncioID)
                .ForeignKey("dbo.Utilizadors", t => t.CriadoPor_UtilizadorID)
                .ForeignKey("dbo.Imovels", t => t.Imovel_ImovelID)
                .Index(t => t.CriadoPor_UtilizadorID)
                .Index(t => t.Imovel_ImovelID);
            
            CreateTable(
                "dbo.Imovels",
                c => new
                    {
                        ImovelID = c.Int(nullable: false, identity: true),
                        Area = c.Single(),
                        Tipologia = c.String(),
                        Local_LocalizacaoID = c.Int(),
                        Tipo_TipoImovelID = c.Int(),
                    })
                .PrimaryKey(t => t.ImovelID)
                .ForeignKey("dbo.Localizacaos", t => t.Local_LocalizacaoID)
                .ForeignKey("dbo.TipoImovels", t => t.Tipo_TipoImovelID)
                .Index(t => t.Local_LocalizacaoID)
                .Index(t => t.Tipo_TipoImovelID);
            
            CreateTable(
                "dbo.Fotoes",
                c => new
                    {
                        FotoID = c.Int(nullable: false, identity: true),
                        Legenda = c.String(),
                        Ficheiro = c.Binary(),
                        Imovel_ImovelID = c.Int(),
                    })
                .PrimaryKey(t => t.FotoID)
                .ForeignKey("dbo.Imovels", t => t.Imovel_ImovelID)
                .Index(t => t.Imovel_ImovelID);
            
            CreateTable(
                "dbo.Localizacaos",
                c => new
                    {
                        LocalizacaoID = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Morada = c.String(),
                        Localidade_RegiaoID = c.Int(),
                    })
                .PrimaryKey(t => t.LocalizacaoID)
                .ForeignKey("dbo.Regiaos", t => t.Localidade_RegiaoID)
                .Index(t => t.Localidade_RegiaoID);
            
            CreateTable(
                "dbo.Regiaos",
                c => new
                    {
                        RegiaoID = c.Int(nullable: false, identity: true),
                        NomeRegiao = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Regiao_RegiaoID = c.Int(),
                    })
                .PrimaryKey(t => t.RegiaoID)
                .ForeignKey("dbo.Regiaos", t => t.Regiao_RegiaoID)
                .Index(t => t.Regiao_RegiaoID);
            
            CreateTable(
                "dbo.PedidoAlertas",
                c => new
                    {
                        PedidoAlertaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Utilizador_UtilizadorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PedidoAlertaID)
                .ForeignKey("dbo.Utilizadors", t => t.Utilizador_UtilizadorID)
                .Index(t => t.Utilizador_UtilizadorID);
            
            CreateTable(
                "dbo.Parametroes",
                c => new
                    {
                        ParametroID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Minimo = c.Single(),
                        Maximo = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        PedidoAlerta_PedidoAlertaID = c.Int(),
                    })
                .PrimaryKey(t => t.ParametroID)
                .ForeignKey("dbo.PedidoAlertas", t => t.PedidoAlerta_PedidoAlertaID)
                .Index(t => t.PedidoAlerta_PedidoAlertaID);
            
            AddColumn("dbo.TipoImovels", "Permuta_AnuncioID", c => c.Int());
            CreateIndex("dbo.TipoImovels", "Permuta_AnuncioID");
            AddForeignKey("dbo.TipoImovels", "Permuta_AnuncioID", "dbo.Anuncios", "AnuncioID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoAlertas", "Utilizador_UtilizadorID", "dbo.Utilizadors");
            DropForeignKey("dbo.Parametroes", "PedidoAlerta_PedidoAlertaID", "dbo.PedidoAlertas");
            DropForeignKey("dbo.Alertas", "Utilizador_UtilizadorID", "dbo.Utilizadors");
            DropForeignKey("dbo.Alertas", "Anuncio_AnuncioID", "dbo.Anuncios");
            DropForeignKey("dbo.TipoImovels", "Permuta_AnuncioID", "dbo.Anuncios");
            DropForeignKey("dbo.Anuncios", "Imovel_ImovelID", "dbo.Imovels");
            DropForeignKey("dbo.Imovels", "Tipo_TipoImovelID", "dbo.TipoImovels");
            DropForeignKey("dbo.Imovels", "Local_LocalizacaoID", "dbo.Localizacaos");
            DropForeignKey("dbo.Localizacaos", "Localidade_RegiaoID", "dbo.Regiaos");
            DropForeignKey("dbo.Regiaos", "Regiao_RegiaoID", "dbo.Regiaos");
            DropForeignKey("dbo.Fotoes", "Imovel_ImovelID", "dbo.Imovels");
            DropForeignKey("dbo.Anuncios", "CriadoPor_UtilizadorID", "dbo.Utilizadors");
            DropIndex("dbo.Parametroes", new[] { "PedidoAlerta_PedidoAlertaID" });
            DropIndex("dbo.PedidoAlertas", new[] { "Utilizador_UtilizadorID" });
            DropIndex("dbo.Regiaos", new[] { "Regiao_RegiaoID" });
            DropIndex("dbo.Localizacaos", new[] { "Localidade_RegiaoID" });
            DropIndex("dbo.Fotoes", new[] { "Imovel_ImovelID" });
            DropIndex("dbo.Imovels", new[] { "Tipo_TipoImovelID" });
            DropIndex("dbo.Imovels", new[] { "Local_LocalizacaoID" });
            DropIndex("dbo.Anuncios", new[] { "Imovel_ImovelID" });
            DropIndex("dbo.Anuncios", new[] { "CriadoPor_UtilizadorID" });
            DropIndex("dbo.Alertas", new[] { "Utilizador_UtilizadorID" });
            DropIndex("dbo.Alertas", new[] { "Anuncio_AnuncioID" });
            DropIndex("dbo.TipoImovels", new[] { "Permuta_AnuncioID" });
            DropColumn("dbo.TipoImovels", "Permuta_AnuncioID");
            DropTable("dbo.Parametroes");
            DropTable("dbo.PedidoAlertas");
            DropTable("dbo.Regiaos");
            DropTable("dbo.Localizacaos");
            DropTable("dbo.Fotoes");
            DropTable("dbo.Imovels");
            DropTable("dbo.Anuncios");
            DropTable("dbo.Alertas");
        }
    }
}
