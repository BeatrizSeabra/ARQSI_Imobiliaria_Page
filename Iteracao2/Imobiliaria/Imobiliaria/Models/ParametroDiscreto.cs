using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Imobiliaria.Models
{
    public class ParametroDiscreto : Parametro
    {
        public ICollection<string> Valores { get; set; }

        public override bool Valida(dynamic valor)
        {
            if (valor.getType() == typeof(string))
            {
                if (valor is string)
                    return Valores.Contains(valor);
                else
                    return false;
            }
            else if (valor.getType() == typeof(ICollection<string>))
            {
                return ValidaCollection(valor);
            }
            return false;
        }

        public bool ValidaCollection(ICollection<string> valores)
        {
            bool res = false;
            object locket = new object();
            Parallel.ForEach(valores, (valor, loopState) =>
            {
                if (Valida(valor))
                {
                    loopState.Stop();
                    lock (locket)
                    {
                        res = true;
                    }
                    return;
                }
            });
            return res;
        }

        public ParametroDiscreto()
        {
            Valores=new List<string>();
        }
    }
}