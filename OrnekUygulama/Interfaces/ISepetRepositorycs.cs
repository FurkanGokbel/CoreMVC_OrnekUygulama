using OrnekUygulama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Interfaces
{
    public interface ISepetRepository
    {
        //SepetRepositoryden aldık
        void SepeteEkle(Urun urun);
        void SepettenCikar(Urun urun);
        List<Urun> GetirSepettekiurunler();
        void SepetiBosalt();

    }
}
