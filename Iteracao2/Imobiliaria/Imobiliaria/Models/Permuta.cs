using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Permuta : Anuncio
    {
        public new string TipoAnuncio { get { return "Permuta"; } }
        public ICollection<TipoImovel> TiposImovel { get; set; }

        public Permuta()
        {
            TiposImovel = new List<TipoImovel>();
        }
    }
}