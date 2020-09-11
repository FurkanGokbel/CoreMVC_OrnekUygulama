using Microsoft.AspNetCore.Razor.TagHelpers;
using OrnekUygulama.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.TagHelpers//viewstart da tanımlamamasını yapman lazım
{
    [HtmlTargetElement("getirKategoriAd")]
    public class KategoriAd : TagHelper
    {
        private readonly IUrunRepository _urunRepository;
        public KategoriAd(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;

        }
        public int UrunId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = "";
            //sadece kategori adlarını getir.
            var gelenKategoriler = _urunRepository.GetirKategoriler(UrunId).Select(i => i.Ad);
            foreach (var item in gelenKategoriler)
            {
                data += item+" ";
            }
            output.Content.SetContent(data);
        }
    }
}
