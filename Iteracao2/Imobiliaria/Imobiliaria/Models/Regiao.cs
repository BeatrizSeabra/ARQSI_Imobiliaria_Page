using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public abstract class Regiao
    {
        public int RegiaoID { get; set; }
        public string NomeRegiao { get; set; }
        public ICollection<Regiao> SubRegioes { get; set; }

    }
}