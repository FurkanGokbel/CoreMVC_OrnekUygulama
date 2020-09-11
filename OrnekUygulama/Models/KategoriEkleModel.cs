using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Models
{
    public class KategoriEkleModel
    {
        [Required(ErrorMessage ="Ad Alanı Boş Bırakılamaz")]
        public string Ad { get; set; }
    }
}
