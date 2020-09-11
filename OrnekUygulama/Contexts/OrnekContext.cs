using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrnekUygulama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekUygulama.Contexts
{
    public class OrnekContext:IdentityDbContext<AppUser>//Klasik bir şekilde Identity tanımlaması yapmadık. AppUser Classını açıp eklemek istediğimiz bir kaç ozelliğimizi ekledik.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-32DSDLJ\\SQLEXPRESS; database=Core_UrunKategori; integrated security=true;");
            base.OnConfiguring(optionsBuilder);//DbContext yerine IdentityDbContext kullanınca bu değişikliği kaydetmek için ekledik
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //üründe ürünkategoriler çoğul ürünün kendisi tek burdaki ilişki UrunID üzerinden
            modelBuilder.Entity<Urun>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Urun).HasForeignKey(I => I.UrunID);
            modelBuilder.Entity<Kategori>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Kategori).HasForeignKey(I => I.KategoriID);

            //Bu aşamada girdiğimiz ürünlerin benzersiz olması için bu kodları yazdık.
            //Örneğin 1 nolu ürün 1 nolu kategoriye 2 kez atanmasını önledik.
            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {
                I.KategoriID,
                I.UrunID
            }).IsUnique();
            base.OnModelCreating(modelBuilder);//DbContext yerine IdentityDbContext kullanınca bu değişikliği kaydetmek için ekledik
        }
        public DbSet<UrunKategori> UrunKategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }
}
