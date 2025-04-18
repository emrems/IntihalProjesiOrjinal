using IntihalProjesi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IntihalProjesi.Repositories.Config
{
    public class OrjinalIntihalDbContext : DbContext
    {
        public OrjinalIntihalDbContext(DbContextOptions<OrjinalIntihalDbContext> options) : base(options) { }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Icerik> Icerikler { get; set; }
        public DbSet<Dosya> Dosyalar { get; set; }
        public DbSet<BenzerlikSonucu> BenzerlikSonuclari { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanici>()
                .HasKey(k => k.KullaniciId);

            // Icerik (Primary Key ve Kullanici ile ilişki)
            modelBuilder.Entity<Icerik>()
                .HasKey(i => i.IcerikId);
            modelBuilder.Entity<Icerik>()
                .HasOne(i => i.Kullanici)
                .WithMany(k => k.Icerikler)
                .HasForeignKey(i => i.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dosya (Primary Key ve ilişkiler)
            modelBuilder.Entity<Dosya>()
                .HasKey(d => d.DosyaId);
            modelBuilder.Entity<Dosya>()
                .HasOne(d => d.Icerik)
                .WithMany(i => i.Dosyalar)
                .HasForeignKey(d => d.IcerikId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Dosya>()
                .HasOne(d => d.Kullanici)
                .WithMany()
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict); 

            // BenzerlikSonucu (Primary Key ve ilişkiler)
            modelBuilder.Entity<BenzerlikSonucu>()
                .HasKey(b => b.SonucId);

            modelBuilder.Entity<BenzerlikSonucu>()
                .HasOne(b => b.IlkDosya)
                .WithMany()
                .HasForeignKey(b => b.IlkDosyaId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<BenzerlikSonucu>()
                .HasOne(b => b.IkinciDosya)
                .WithMany()
                .HasForeignKey(b => b.IkinciDosyaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BenzerlikSonucu>()
                .HasOne(b => b.Icerik)
                .WithMany()
                .HasForeignKey(b => b.IcerikId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bildirim (Primary Key ve ilişkiler)
            modelBuilder.Entity<Bildirim>()
                .HasOne(b => b.Kullanici)  
                .WithMany(k => k.Bildirimler) 
                .HasForeignKey(b => b.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);  

           

            // Initial Data (Veritabanına ilk veri eklemek)
            modelBuilder.Entity<Kullanici>().HasData(
                new Kullanici
                {
                    KullaniciId = 1,
                    Ad = "emre",
                    Soyad = "almamış",
                    Eposta = "emre@gmail.com",
                    Sifre = "emre123",
                    Rol = "Admin"
                }
            );
        }
    }
}
