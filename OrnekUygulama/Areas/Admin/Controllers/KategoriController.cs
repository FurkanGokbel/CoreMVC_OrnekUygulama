using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using OrnekUygulama.Models;

namespace OrnekUygulama.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriController(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        public IActionResult Index()
        {
            return View(_kategoriRepository.GetirHepsi());
        }
        public IActionResult Ekle()
        {
            return View(new KategoriEkleModel());//Kategori EKlemek için Model Ekliyoruz
        }
        [HttpPost]
        public IActionResult Ekle(KategoriEkleModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoriRepository.Ekle(new Kategori
                {
                    Ad = model.Ad
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Guncelle(int Id)
        {
            var gelenKategori = _kategoriRepository.GetirId(Id);
            KategoriGuncelleModel model = new KategoriGuncelleModel
            {
                Ad = gelenKategori.Ad,
                Id = gelenKategori.ID

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(KategoriGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKategori = _kategoriRepository.GetirId(model.Id);
                guncellenecekKategori.Ad = model.Ad;
                _kategoriRepository.Guncelle(guncellenecekKategori);
                return RedirectToAction("Index", "Kategori", new { area = "Admin" });//area yı belirtmemize gerek yok otomatik gider zaten
            }
            return View(model);
        }
        public IActionResult Sil(int id)
        {
            _kategoriRepository.Sil(new Kategori { ID = id });
            return RedirectToAction("Index");
        }
        //Bu controlleri kullanabilmen için startup da tanımlaman lazım

    }
}
