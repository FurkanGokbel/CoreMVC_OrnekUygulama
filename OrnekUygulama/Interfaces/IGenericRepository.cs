using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Interfaces
{
    public interface IGenericRepository<Tablo> where Tablo : class, new()
    {
        void Ekle(Tablo tablo);
        void Sil(Tablo tablo);
        void Guncelle(Tablo tablo);
        public List<Tablo> GetirHepsi();
        public Tablo GetirId(int ID);



    }
}
