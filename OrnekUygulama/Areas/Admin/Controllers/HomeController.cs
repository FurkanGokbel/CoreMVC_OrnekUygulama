using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using OrnekUygulama.Models;

namespace OrnekUygulama.Areas.Admin.Controllers
//Bu controllerin amacı diğer controllerin yükünü azaltmaktır.
{
    [Area("Admin")]//bunu yazarak diğer HomeControllerin çalışmasını sağladık
    [Authorize]//Sadece giriş yapmış kullanıcılar erişebilir. Startupda eklemeler yapmalısın
    public class HomeController : Controller
    {
        private readonly IUrunRepository _urunRepository;
        private readonly IKategoriRepository _kategoriRepository;   //Ürüne ait kategoriler tüm kategorilerde varmı kontrol etmemiz gerek    

        public HomeController(IUrunRepository urunRepository, IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
            _urunRepository = urunRepository;
        }
        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }
        public IActionResult Ekle()
        {
            return View(new UrunEkleModel());
        }
        [HttpPost]
        public IActionResult Ekle(UrunEkleModel model)
        {
            if (ModelState.IsValid)
            {
                Urun urun = new Urun();
                if (model.Resim != null)
                //if (model.Resim.ContentType=="image/jpeg") =>Sadece jpeg dosyalarını belirtme işlemi
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;//aynı adlı resimden iki tane olmaması lazım
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img/" + yeniResimAd);
                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    model.Resim.CopyTo(stream);
                    urun.Resim = yeniResimAd;
                }
                urun.Ad = model.Ad;
                urun.Fiyat = model.Fiyat;
                _urunRepository.Ekle(urun);
                return RedirectToAction("Index", "Home", new { area = "Admin" });//area yı belirtmemize gerek yok otomatik gider zaten
            }
            return View(model);
        }
        public IActionResult Guncelle(int Id)
        {
            var gelenUrun = _urunRepository.GetirId(Id);
            UrunGuncelleModel model = new UrunGuncelleModel
            {
                Ad = gelenUrun.Ad,
                Fiyat = gelenUrun.Fiyat,
                Id = gelenUrun.ID

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(UrunGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _urunRepository.GetirId(model.Id);
                if (model.Resim != null)
                //if (model.Resim.ContentType=="image/jpeg") =>Sadece jpeg dosyalarını belirtme işlemi
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);
                    var yeniResimAd = Guid.NewGuid() + uzanti;//aynı adlı resimden iki tane olmaması lazım
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/img/" + yeniResimAd);
                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    model.Resim.CopyTo(stream);
                    guncellenecekUrun.Resim = yeniResimAd;
                }

                guncellenecekUrun.Ad = model.Ad;
                guncellenecekUrun.Fiyat = model.Fiyat;
                _urunRepository.Guncelle(guncellenecekUrun);
                return RedirectToAction("Index", "Home", new { area = "Admin" });//area yı belirtmemize gerek yok otomatik gider zaten
            }
            return View(model);
        }
        public IActionResult Sil(int id)
        {
            _urunRepository.Sil(new Urun { ID = id });
            return RedirectToAction("Index");
        }
        public IActionResult AtaKategori(int id)
        {
            var uruneAitKategoriler = _urunRepository.GetirKategoriler(id).Select(I => I.Ad);
            var kategoriler = _kategoriRepository.GetirHepsi();
            TempData["UrunID"] = id;
            List<KategoriAtaModel> list = new List<KategoriAtaModel>();
            foreach (var item in kategoriler)
            {
                KategoriAtaModel model = new KategoriAtaModel();
                model.KategoriId = item.ID;
                model.KategoriAd = item.Ad;
                model.Varmi = uruneAitKategoriler.Contains(item.Ad);
                list.Add(model);
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult AtaKategori(List<KategoriAtaModel> list)
        {
            int urunId = (int)TempData["UrunID"];
            foreach (var item in list)
            {
                if (item.Varmi)// kulanıcı click atmışşsa
                {
                    _urunRepository.EkleKategori(new UrunKategori
                    {
                        KategoriID = item.KategoriId,
                        UrunID = urunId//tempdata kullanmamız gerekir
                    });
                }
                else
                {
                    _urunRepository.SilKategori(new UrunKategori
                    {
                        KategoriID = item.KategoriId,
                        UrunID = urunId//tempdata kullanmamız gerekir
                    });
                }

            }
            return RedirectToAction("Index");

        }
        //Bu controlleri kullanabilmen için startup da tanımlaman lazım
    }
}
