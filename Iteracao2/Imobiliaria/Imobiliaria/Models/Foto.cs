using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Foto
    {
        public int FotoID { get; set; }
        public string Legenda { get; set; }
        public byte[] Ficheiro { get; set; }
    }
}