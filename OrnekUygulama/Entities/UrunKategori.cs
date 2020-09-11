using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Entities
{
    public class UrunKategori
    {
        public int ID { get; set; }
        public int UrunID { get; set; }
        public Urun Urun { get; set; }
        public int KategoriID { get; set; }
        public Kategori Kategori { get; set; }

    }
}
