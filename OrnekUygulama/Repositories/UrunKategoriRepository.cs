using OrnekUygulama.Contexts;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrnekUygulama.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>, IUrunKategoriRepository
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context = new OrnekContext();
            return context.UrunKategoriler.FirstOrDefault(filter);//kendisine gelen filtre ile tek bir kayıt getiren metot yazdık
        }
    }
}
