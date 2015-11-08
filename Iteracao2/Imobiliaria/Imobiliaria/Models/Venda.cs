using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Venda : Anuncio
    {
        public new string TipoAnuncio { get { return "Venda"; } }
    }
}