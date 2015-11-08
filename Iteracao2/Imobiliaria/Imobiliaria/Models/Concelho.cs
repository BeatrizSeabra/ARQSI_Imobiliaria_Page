using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Concelho : Regiao
    {
        public new ICollection<Freguesia> SubRegioes { get; set; }

        public Concelho()
        {
            SubRegioes = new List<Freguesia>();
        }
    }
}