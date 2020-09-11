using OrnekUygulama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Interfaces
{
    public interface IUrunRepository:IGenericRepository<Urun>
    {
        public List<Kategori> GetirKategoriler(int urunId);
        void EkleKategori(UrunKategori urunKategori);
        void SilKategori(UrunKategori urunKategori);
        List<Urun> GetirKategoriIdile(int kategoriId);

    }
}
