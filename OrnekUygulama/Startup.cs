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
            services.AddDbContext<OrnekContext>();//Identity i�in  tan�mlama yapt�k
            services.AddHttpContextAccessor();//HttpCOntext e eri�mek i�in kulland�k(SepetRepository)
            services.AddAuthorization();//[Authorize] i�lemi i�in ekledik 

            //Tek bir context �zeriden Identity i�lemini y�r�tt�k. B�yle yapmasak 2 context ve ayr� database a��l�cakt�
            //opt ile �zelle�tirme i�lemlerimizi yap�yoruz
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;//  say�sal bir de�er olmas� zorunlumu
                opt.Password.RequireLowercase = false;//k���k harf olmas� zorunlumu
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;//�ifremizi 123 gibi tan�mlayabilece�iz

            })
                .AddEntityFrameworkStores<OrnekContext>();//Identity tan�mlamas� yap�yoruz

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/GirisYap");//admin giri� yapt���m zaman beni bu yola y�nlendir
                opt.Cookie.Name = "AspNetCoreOrnek";
                opt.Cookie.HttpOnly = true;//bu cookie javascript kodlar� ile �ekilemesin
                opt.Cookie.SameSite = SameSiteMode.Strict;//bu cookie hi�bir yerde kullan�lamaz
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);//30 dakika boyunca bu cookie bilgisini tutucaz
            });

            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<ISepetRepository,SepetRepository>();
            services.AddScoped<IKategoriRepository, KategoriRepository>();
            services.AddScoped<IUrunKategoriRepository, UrunKategoriRepository>();
            services.AddSession();//sessionu kullanabilmek i�in bunu eklemek zorunday�z
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)// olusturadmin usermanager ve rolemanager istedi�i i�in tan�mlad�k.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");//yanl�� sayfaya gittin mi d�z beyaz ekran gelmez

            IdentityInitializer.OlusturAdmin(userManager, roleManager);
            app.UseRouting();
            app.UseSession();//Sessionu belirtmemiz gerekir
            app.UseAuthentication();//[Authorize] i�lemi i�in ekledik.�lgili kullan�c� giri� yap�p yapmad���n� kontrol eder
            app.UseAuthorization();//[Authorize] i�lemi i�in ekledik. Hangi tip kullan�c� oldu�unu belirler(admin,user).
            
            app.UseStaticFiles();
            //ornek.com/furkan      =>OZEL
            //ornek.com/Home/Index/furkan       =>GENEL
            app.UseEndpoints(endpoints =>
            {
                //Ozel yukar� genel a�a�� yaz�lmal�

                //OZEL
                endpoints.MapControllerRoute(name: "furkan",
                    pattern: "furkan",
                    defaults: new { Controller = "Home", Action = "Index" });

                //GENEL
                //area tan�mlama i�lemi
                endpoints.MapControllerRoute(name: "areas", pattern:
                    "{area}/{Controller=Home}/{Action=Index}/{id?}");

                //id? k�s�tlama olmadan getir demek
                //EN GENEL
                endpoints.MapControllerRoute(name: "default", pattern:
                    "{Controller=Home}/{Action=Index}/{id?}");


            });
        }
    }
}
