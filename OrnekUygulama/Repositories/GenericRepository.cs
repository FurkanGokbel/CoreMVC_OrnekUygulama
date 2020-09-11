using OrnekUygulama.Contexts;
using OrnekUygulama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Repositories
{

    public class GenericRepository<Tablo> where Tablo : class, new()
    {
        public void Ekle(Tablo tablo)
        {
            //using, kullanve rami boşalt
            using var context = new OrnekContext();
            context.Set<Tablo>().Add(tablo);
            context.SaveChanges();
        }
        public void Guncelle(Tablo tablo)
        {
            //using, kullanve rami boşalt
            using var context = new OrnekContext();
            context.Set<Tablo>().Update(tablo);
            context.SaveChanges();
        }
        public void Sil(Tablo tablo)
        {
            //using, kullanve rami boşalt
            using var context = new OrnekContext();
            context.Set<Tablo>().Remove(tablo);
            context.SaveChanges();
        }
        public List<Tablo> GetirHepsi()
        {
            using var context = new OrnekContext();
            return context.Set<Tablo>().ToList();
        }

        public Tablo GetirId(int ID)
        {
            using var context = new OrnekContext();
            return context.Set<Tablo>().Find(ID);
        }
    }
}
