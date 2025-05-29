using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class BenzerlikSonuclariRepository : RepositoryBase<BenzerlikSonucu>, IBenzerlikSonuclariRepository
    {
        private readonly OrjinalIntihalDbContext _context;

        public BenzerlikSonuclariRepository(OrjinalIntihalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<dynamic>> GetBenzerlikSonuclariByIcerikIdAsync(int icerikId)
        {
            var query = await (from b in _context.BenzerlikSonuclari
                               join d1 in _context.Dosyalar on b.IlkDosyaId equals d1.DosyaId
                               join d2 in _context.Dosyalar on b.IkinciDosyaId equals d2.DosyaId
                               join k1 in _context.Kullanicilar on d1.KullaniciId equals k1.KullaniciId
                               join k2 in _context.Kullanicilar on d2.KullaniciId equals k2.KullaniciId
                               where d1.IcerikId == icerikId || d2.IcerikId == icerikId
                               select new
                               {
                                   IcerikId = icerikId,                    
                                   BenzerlikOrani = b.BenzerlikOrani,
                                   IlkKullaniciAdiSoyad = k1.Ad + " " + k1.Soyad,
                                   IkinciKullaniciAdiSoyad = k2.Ad + " " + k2.Soyad,
                                   IlkDosyaCleanPath = d1.CleanedPath,
                                   IkinciDosyaCleanPath = d2.CleanedPath,
                                   file1_id = b.IlkDosyaId,
                                   file2_id = b.IkinciDosyaId,
                               })
                              .ToListAsync();

            return query.Cast<dynamic>().ToList();
        }



    }
}
