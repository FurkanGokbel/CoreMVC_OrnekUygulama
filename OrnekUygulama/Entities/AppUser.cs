using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Entities
{
    public class AppUser:IdentityUser
    {
        //Klasik Identity üstüne bunlarıda ekle
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
