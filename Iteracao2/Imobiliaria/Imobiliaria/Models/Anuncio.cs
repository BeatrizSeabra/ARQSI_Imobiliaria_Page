using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public abstract class Anuncio
    {
        public int AnuncioID { get; set; }
        public string Mediador { get; set; }
        public Imovel Imovel { get; set; }
        public Utilizador CriadoPor { get; set; }
        public DateTime CriadoEm { get; set; }
        public float? Preco { get; set; }
        public string TipoAnuncio;
    }
}