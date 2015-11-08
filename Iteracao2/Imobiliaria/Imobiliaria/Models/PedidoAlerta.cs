using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Imobiliaria.Models
{
    public class PedidoAlerta
    {
        public int PedidoAlertaID { get; set; }
        public ICollection<Parametro> Parametros { get; set; }
        public string Nome { get; set; }

        internal bool Valida(Anuncio anuncio)
        {
            bool res = true;
            object locket = new object();
            Parallel.ForEach(Parametros, (filtro, loopState) =>
            {
                bool fres = true;
                switch (filtro.Nome)
                {
                    case "Preco": fres = anuncio.Preco != null ? filtro.Valida(anuncio.Preco.Value) : true; break;
                    case "Area": fres = anuncio.Imovel.Area != null ? filtro.Valida(anuncio.Imovel.Area) : true; break;
                    case "TipoImovel": fres = filtro.Valida(getTipoImovelDescendencia(anuncio.Imovel.Tipo)); break;
                    case "TipoAnuncio": fres = filtro.Valida(anuncio.TipoAnuncio); break;
                    case "Mediador": fres = filtro.Valida(anuncio.Mediador); break;
                    case "Tipologia": fres = filtro.Valida(anuncio.Imovel.Tipologia); break;
                    case "Localidade": fres = filtro.Valida(getRegiaoDescendencia(anuncio.Imovel.Local.Localidade)); break;
                    default: break;
                }

                if (!fres)
                {
                    loopState.Stop();
                    lock (locket)
                    {
                        res = false;
                    }
                    return;
                }
            });
            return res;
        }

        public PedidoAlerta()
        {
            Parametros = new List<Parametro>();
        }

        private ICollection<string> getTipoImovelDescendencia(TipoImovel tipo)
        {
            List<string> descendentes = new List<string>();
            object locket = new object();
            descendentes.Add(tipo.NomeTipo);
            Parallel.ForEach(tipo.SubTipos, (subtipo) =>
            {
                List<string> subdescendentes = new List<string>();
                subdescendentes.AddRange(getTipoImovelDescendencia(subtipo));
                lock (locket)
                {
                    descendentes.AddRange(subdescendentes);
                }
            });
            return descendentes;
        }

        private ICollection<string> getRegiaoDescendencia(Regiao regiao)
        {
            List<string> descendentes = new List<string>();
            object locket = new object();
            descendentes.Add(regiao.NomeRegiao);

            Parallel.ForEach(regiao.SubRegioes, (subtipo) =>
            {

                List<string> subdescendentes = new List<string>();
                subdescendentes.AddRange(getRegiaoDescendencia(subtipo));
                lock (locket)
                {
                    descendentes.AddRange(subdescendentes);
                }
            });
            return descendentes;
        }


    }
}