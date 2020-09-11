using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using OrnekUygulama.Models;

namespace OrnekUygulama.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUrunRepository _urunRepository;
        private readonly ISepetRepository _sepetRepository;
        public HomeController(IUrunRepository urunRepository,
            SignInManager<AppUser> signInManager, ISepetRepository sepetRepository)
        {
            _sepetRepository = sepetRepository;
            _signInManager = signInManager;
            _urunRepository = urunRepository;
        }
        public IActionResult Index(int? kategoriId)
        {
            ViewBag.KategoriId = kategoriId;
            //SetCookie("kisi","furkan");
            // SetSession("kisi","furkan");
            return View();
        }

        //Startapp da /{id?} eklentisini yapmamız lazım
        public IActionResult UrunDetay(int id)
        {
            //ViewBag.Cookie = GetCookie("kisi");
            //ViewBag.Session = GetSession("kisi");
            return View(_urunRepository.GetirId(id));
        }
        public IActionResult GirisYap()
        {
            return View(new KullaniciGirisModel());
        }
        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisModel model)
        {
            if (ModelState.IsValid == true)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd,
                     model.Sifre, model.BeniHatirla, false).Result;
                //true yapsaydık kullanıcı hatalı girişinde 5 dakka boyunca giremez gibi bie durumu yapabilirdik
                //isNotAllowed mail dogrulama, telefon dogrulama adımları gibi
                //isLockedAllowed kullanıcı kilitli mi değilmi
                //RequiresTwo factor ikili doğrulama
                //Succueeded başarılı olma işlemi
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View(model);
        }
        public IActionResult EkleSepet(int id)
        {
            var urun = _urunRepository.GetirId(id);
            _sepetRepository.SepeteEkle(urun);
            TempData["bildirim"] = "Ürün Sepete Eklendi";
            return RedirectToAction("Index");
        }
        public IActionResult Sepet()//Sepeti Görüntülemek
        {

            return View(_sepetRepository.GetirSepettekiurunler());
        }
        public IActionResult SepetiBosalt(decimal fiyat)//Sepeti Görüntülemek
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Tesekkur", new { fiyat = fiyat });
        }
        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }

        public IActionResult SepettenCikar(int id)
        {
            var cikarilacakUrun = _urunRepository.GetirId(id);
            _sepetRepository.SepettenCikar(cikarilacakUrun);
            return RedirectToAction("sepet");
        }
        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }

        /*          Session İşlemleri
        public void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
        */

        //-------------------------------------------------------------------------

        /*                         Cookie İşlemleri              */
        //public void SetCookie (string key,string value)
        //{
        //    HttpContext.Response.Cookies.Append(key, value);
        //}
        //public string GetCookie (string key)
        //{
        //    HttpContext.Request.Cookies.TryGetValue(key, out string value);
        //    return value;
        //} 

    }


}
