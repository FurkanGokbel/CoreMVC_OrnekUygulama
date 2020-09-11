using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using OrnekUygulama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Repositories
{
    public class DpUrunRepository
    {
        public List<Urun> GetirHepsi()
        {
            using var connection = new SqlConnection("server=DESKTOP-32DSDLJ\\SQLEXPRESS;" +
                " database=Core_UrunKategori; integrated security=true;");
            //ürünleri getir dapper ile
            return connection.GetAll<Urun>().ToList();
        }

    }
}
