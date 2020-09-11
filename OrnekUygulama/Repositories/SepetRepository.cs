using Microsoft.AspNetCore.Http;
using OrnekUygulama.CustomExtensions;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Repositories
{
    //startup a eklenti yapman lazım
    
    public class SepetRepository:ISepetRepository//bunu ekledikten sonra ilgili bağımlılığı startupta belirtmen lazım
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SepetRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SepeteEkle(Urun urun)
        {
           var gelenListe= _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            if (gelenListe==null)
            {
                gelenListe = new List<Urun>();
                gelenListe.Add(urun);
            }
            else
            {
                gelenListe.Add(urun);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }

        public void SepettenCikar(Urun urun)
        {
           var gelenListe= _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            gelenListe.Remove(urun);
            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }
        public List<Urun> GetirSepettekiurunler()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
        }
        public void SepetiBosalt()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sepet");
        }

    }
}
