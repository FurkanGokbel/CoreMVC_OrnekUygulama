using OrnekUygulama.Contexts;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OrnekUygulama.Repositories
{
    public class UrunRepository : GenericRepository<Urun>, IUrunRepository
    {
        private readonly IUrunKategoriRepository _urunKategoriRepository;
        public UrunRepository(IUrunKategoriRepository urunKategoriRepository)
        {
            _urunKategoriRepository = urunKategoriRepository;
        }

        public List<Kategori> GetirKategoriler(int urunId)//IurunRepository kısmında belirttik
        {
            //çoktan çok ilişkiyi belirttik
            using var context = new OrnekContext();
            return context.Urunler.Join(context.UrunKategoriler, urun => urun.ID,
                urunkategori => urunkategori.UrunID, (u, uc) => new
                {
                    urun = u,
                    urunKategori = uc
                }).Join(context.Kategoriler, iki => iki.urunKategori.KategoriID, kategori =>
                    kategori.ID, (uc, k) => new
                    {
                        urun = uc.urun,
                        kategori = k,
                        urunKategori = uc.urunKategori
                    }).Where(I => I.urun.ID == urunId).Select(I => new Kategori
                    {
                        Ad = I.kategori.Ad,
                        ID = I.kategori.ID
                    }).ToList();
        }

        public void SilKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile(I => I.KategoriID ==
               urunKategori.KategoriID && I.UrunID == urunKategori.UrunID);
            if (kontrolKayit != null)
            {
                _urunKategoriRepository.Sil(kontrolKayit);
            }
        }
        public void EkleKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile(I => I.KategoriID ==
             urunKategori.KategoriID && I.UrunID == urunKategori.UrunID);
            if (kontrolKayit == null)
            {
                _urunKategoriRepository.Ekle(urunKategori);
            }
        }

        public List<Urun> GetirKategoriIdile(int kategoriId)
        {
            using var context = new OrnekContext();
            return context.Urunler.Join(context.UrunKategoriler, u => u.ID, uc => uc.UrunID,
                 (urun, urunKategori) => new
                 {
                     Urun = urun,
                     UrunKategori = urunKategori

                 }).Where(I => I.UrunKategori.KategoriID == kategoriId).Select(I => new Urun
                 {
                     ID = I.Urun.ID,
                     Ad = I.Urun.Ad,
                     Fiyat = I.Urun.Fiyat,
                     Resim = I.Urun.Resim

                 }).ToList();

        }
    }
}
