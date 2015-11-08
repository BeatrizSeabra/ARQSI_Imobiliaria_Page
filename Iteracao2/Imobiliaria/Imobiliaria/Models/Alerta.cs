using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Alerta
    {
        public int AlertaID { get; set; }
        public Anuncio Anuncio { get; set; }
        public bool Lido { get; set; }
    }
}