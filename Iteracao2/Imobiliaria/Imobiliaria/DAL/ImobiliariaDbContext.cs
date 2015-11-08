using Imobiliaria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Imobiliaria.DAL
{
    public class ImobiliariaDBContext : DbContext
    {
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Concelho> Concelhos { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Freguesia> Freguesias { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<ParametroContinuo> ParametrosContinuos { get; set; }
        public DbSet<ParametroDiscreto> ParametrosDiscretos { get; set; }
        public DbSet<PedidoAlerta> PedidosAlerta { get; set; }
        public DbSet<Permuta> Permutas { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<TipoImovel> TiposImovel { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
    }
}