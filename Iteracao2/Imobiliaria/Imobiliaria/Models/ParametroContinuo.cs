using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class ParametroContinuo : Parametro
    {
        public float? Minimo { get; set; }
        public float? Maximo { get; set; }

        public override bool Valida(dynamic valor)
        {
            if (valor.getType() != typeof(float))
            {
                return false;
            }
            bool res = true;

            res &= Minimo != null ? valor >= Minimo : true;
            res &= Maximo != null ? valor <= Maximo : true;

            return res;
        }
    }
}