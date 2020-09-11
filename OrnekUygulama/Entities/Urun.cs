using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Entities
{
    [Dapper.Contrib.Extensions.Table("Urunler")]
    public class Urun:IEquatable<Urun>
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Ad { get; set; }
        [MaxLength(250)]
        public string Resim{ get; set; }
        public decimal Fiyat{ get; set; }

        public List<UrunKategori> UrunKategoriler { get; set; }

        public bool Equals([AllowNull] Urun other)//Bunu yapmadığımızda sepetten çıkarma işleminde sorun çıktı. Bunu yapmak zorundayız
        {
            return ID == other.ID && Ad == other.Ad && Resim == other.Resim && Fiyat == other.Fiyat;
        }
    }
}
