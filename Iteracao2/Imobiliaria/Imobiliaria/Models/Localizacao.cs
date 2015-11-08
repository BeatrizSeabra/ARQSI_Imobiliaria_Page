using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Localizacao
    {
        public int LocalizacaoID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Morada { get; set; }
        public Regiao Localidade { get; set; }
    }
}