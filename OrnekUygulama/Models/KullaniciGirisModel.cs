using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Models
{
    public class KullaniciGirisModel
    {
        [Required(ErrorMessage = "Kullanici Adi Bos Gecilemez")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Sifre Bos Gecilemez")]
        public string Sifre { get; set; }

        public bool BeniHatirla { get; set; }
    }
}
