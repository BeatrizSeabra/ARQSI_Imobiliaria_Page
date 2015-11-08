using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Compra : Anuncio
    {
        public new string TipoAnuncio { get { return "Compra"; } }
    }
}