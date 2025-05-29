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
        public DbSet<JplagJob> JplagJobs { get; set; }
        public DbSet<ComparisonJsonDetail> ComparisonJsonDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JplagJob>()
            .HasKey(j => j.JobId);
            modelBuilder.Entity<JplagJob>()
                .HasOne(j => j.Icerik)
                .WithMany(i => i.JplagJobs)
                .HasForeignKey(j => j.IcerikId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kullanici>()
                .HasKey(k => k.KullaniciId);

            modelBuilder.Entity<Icerik>()
                .HasKey(i => i.IcerikId);
            modelBuilder.Entity<Icerik>()
                .HasOne(i => i.Kullanici)
                .WithMany(k => k.Icerikler)
                .HasForeignKey(i => i.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Dosya>()
                .HasKey(d => d.DosyaId);
            modelBuilder.Entity<Dosya>()
                .HasOne(d => d.Icerik)
                .WithMany(i => i.Dosyalar)
                .HasForeignKey(d => d.IcerikId)
                .OnDelete(DeleteBehavior.Restrict);

            // BU SATIR KRİTİK! Cascade yerine SetNull
            modelBuilder.Entity<Dosya>()
                .HasOne(d => d.Kullanici)
                .WithMany()
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BenzerlikSonucu>()
                .HasKey(b => b.SonucId);
            modelBuilder.Entity<BenzerlikSonucu>()
                .HasOne(b => b.IlkDosya)
                .WithMany()
                .HasForeignKey(b => b.IlkDosyaId)
                .OnDelete(DeleteBehavior.SetNull);
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

            modelBuilder.Entity<Bildirim>()
                .HasOne(b => b.Kullanici)
                .WithMany(k => k.Bildirimler)
                .HasForeignKey(b => b.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            // Icerik ile ilişki
            modelBuilder.Entity<ComparisonJsonDetail>()
                .HasOne(c => c.Content)
                .WithMany(i => i.ComparisonJsonDetails)
                .HasForeignKey(c => c.ContentId)
                .OnDelete(DeleteBehavior.Restrict);

            // FirstFile ile ilişki
            modelBuilder.Entity<ComparisonJsonDetail>()
                .HasOne(c => c.FirstFile)
                .WithMany(d => d.FirstFileComparisons)
                .HasForeignKey(c => c.FirstFileId)
                .OnDelete(DeleteBehavior.Restrict);

            // SecondFile ile ilişki
            modelBuilder.Entity<ComparisonJsonDetail>()
                .HasOne(c => c.SecondFile)
                .WithMany(d => d.SecondFileComparisons)
                .HasForeignKey(c => c.SecondFileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Kullanici>().HasData(
                new Kullanici
                {
                    KullaniciId = 1,
                    Ad = "emre",
                    Soyad = "almamış",
                    Eposta = "emre@gmail.com",
                    Sifre = "emre123",
                    Rol = "Admin"
                });

        }
    }
}
