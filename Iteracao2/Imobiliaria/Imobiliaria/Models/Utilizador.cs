using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Utilizador
    {
        public string UtilizadorID { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public ICollection<Alerta> Alertas { get; set; }
        public ICollection<PedidoAlerta> PedidosAlerta { get; set; }

        public Utilizador()
        {
            Alertas = new List<Alerta>();
            PedidosAlerta = new List<PedidoAlerta>();
        }
    }
}