using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Distrito : Regiao
    {
        public new ICollection<Concelho> SubRegioes { get; set; }

        public Distrito()
        {
            SubRegioes = new List<Concelho>();
        }
    }
}