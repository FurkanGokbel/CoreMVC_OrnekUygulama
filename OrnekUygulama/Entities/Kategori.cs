using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Entities
{
    public class Kategori
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Ad { get; set; }


        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
