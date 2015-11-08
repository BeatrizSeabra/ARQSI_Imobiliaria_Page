using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Imovel
    {
        public int ImovelID { get; set; }
        public Localizacao Local { get; set; }
        public float? Area { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public TipoImovel Tipo { get; set; }
        public string Tipologia { get; set; }

        public Imovel()
        {
            Fotos = new List<Foto>();
        }
    }
}