using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class TipoImovel
    {
        public int TipoImovelID { get; set; }
        public string NomeTipo { get; set; }
        public ICollection<TipoImovel> SubTipos { get; set; }

        public TipoImovel()
        {
            SubTipos = new List<TipoImovel>();
        }

    }
}