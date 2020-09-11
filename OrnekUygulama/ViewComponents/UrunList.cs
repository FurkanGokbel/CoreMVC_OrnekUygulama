using Microsoft.AspNetCore.Mvc;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.ViewComponents
{
    public class UrunList:ViewComponent
    {
        private readonly IUrunRepository _urunRepository;
        public UrunList (IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IViewComponentResult Invoke(int? kategoriId)//Kategorilere tıklayonca onların parametrelerini al
        {
            if (kategoriId.HasValue)
            {
                return View(_urunRepository.GetirKategoriIdile((int)kategoriId));
            }
            return View(_urunRepository.GetirHepsi());
        }
    }
}
