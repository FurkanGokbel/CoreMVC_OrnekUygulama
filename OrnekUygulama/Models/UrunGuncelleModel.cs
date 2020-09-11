using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Models
{
    public class UrunGuncelleModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad alanı gerekli")]
        public string Ad { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Fiyat 0 dan yüksek olmalıdır")]
        public decimal Fiyat { get; set; }
        public IFormFile Resim { get; set; }
    }
}
