using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public abstract class Parametro
    {
        public int ParametroID { get; set; }
        public string Nome { get; set; }

        public abstract bool Valida(dynamic valor);
    }
}