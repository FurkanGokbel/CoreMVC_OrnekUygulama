using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using OrnekUygulama.Contexts;
using OrnekUygulama.Entities;
using OrnekUygulama.Interfaces;
using OrnekUygulama.Repositories;

namespace OrnekUygulama
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrnekContext>();//Identity için  tanýmlama yaptýk
            services.AddHttpContextAccessor();//HttpCOntext e eriþmek için kullandýk(SepetRepository)
            services.AddAuthorization();//[Authorize] iþlemi için ekledik 

            //Tek bir context üzeriden Identity iþlemini yürüttük. Böyle yapmasak 2 context ve ayrý database açýlýcaktý
            //opt ile özelleþtirme iþlemlerimizi yapýyoruz
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;//  sayýsal bir deðer olmasý zorunlumu
                opt.Password.RequireLowercase = false;//küçük harf olmasý zorunlumu
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;//þifremizi 123 gibi tanýmlayabileceðiz

            })
                .AddEntityFrameworkStores<OrnekContext>();//Identity tanýmlamasý yapýyoruz

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/GirisYap");//admin giriþ yaptýðým zaman beni bu yola yönlendir
                opt.Cookie.Name = "AspNetCoreOrnek";
                opt.Cookie.HttpOnly = true;//bu cookie javascript kodlarý ile çekilemesin
                opt.Cookie.SameSite = SameSiteMode.Strict;//bu cookie hiçbir yerde kullanýlamaz
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);//30 dakika boyunca bu cookie bilgisini tutucaz
            });

            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<ISepetRepository,SepetRepository>();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddSession();//sessionu kullanabilmek için bunu eklemek zorundayýz
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)// olusturadmin usermanager ve rolemanager istediði için tanýmladýk.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");//yanlýþ sayfaya gittin mi düz beyaz ekran gelmez

            IdentityInitializer.OlusturAdmin(userManager, roleManager);
            app.UseRouting();
            app.UseSession();//Sessionu belirtmemiz gerekir
            app.UseAuthentication();//[Authorize] iþlemi için ekledik.Ýlgili kullanýcý giriþ yapýp yapmadýðýný kontrol eder
            app.UseAuthorization();//[Authorize] iþlemi için ekledik. Hangi tip kullanýcý olduðunu belirler(admin,user).
            
            app.UseStaticFiles();
            //ornek.com/furkan      =>OZEL
            //ornek.com/Home/Index/furkan       =>GENEL
            app.UseEndpoints(endpoints =>
            {
                //Ozel yukarý genel aþaðý yazýlmalý

                //OZEL
                endpoints.MapControllerRoute(name: "furkan",
                    pattern: "furkan",
                    defaults: new { Controller = "Home", Action = "Index" });

                //GENEL
                //area tanýmlama iþlemi
                endpoints.MapControllerRoute(name: "areas", pattern:
                    "{area}/{Controller=Home}/{Action=Index}/{id?}");

                //id? kýsýtlama olmadan getir demek
                //EN GENEL
                endpoints.MapControllerRoute(name: "default", pattern:
                    "{Controller=Home}/{Action=Index}/{id?}");


            });
        }
    }
}
