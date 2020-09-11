using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Models
{
    public class KategoriAtaModel
        //ana amaç birden fazla kategoriyi bir bloga atamak
    {
        public int KategoriId { get; set; }
        public string KategoriAd { get; set; }
        public bool Varmi { get; set; }
    }
}
