
using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class BildirimRepository : RepositoryBase<Bildirim>, IBildirimRepository
    {
        public BildirimRepository(OrjinalIntihalDbContext context) : base(context)
        {

        }

        public async Task AddNotificationAsync(Bildirim bildirim)
        {
            _context.Bildirimler.Add(bildirim);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BildirimDTO>> GetAllBildirimlerAsync()
        {
            return await _context.Bildirimler
                .Include(b => b.Kullanici)
                .Where(b => !b.Okundu)
                .Select(b => new BildirimDTO
                {
                    BildirimId = b.BildirimId,
                    Mesaj = b.Mesaj,
                    OlusturmaTarihi = b.OlusturmaTarihi,
                    KullaniciAdi = b.Kullanici.Ad + " " + b.Kullanici.Soyad
                    
                })
                .ToListAsync();
        }

        // admin panelinde hangi kullanıcıya hangi bildirim gittiğini görmek için
        public async Task<List<BildirimDTO>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Bildirimler
            .Where(b => b.KullaniciId == userId && !b.Okundu)
            .Select(b => new BildirimDTO
            {
                BildirimId = b.BildirimId,
                Mesaj = b.Mesaj,
                OlusturmaTarihi = b.OlusturmaTarihi,
                KullaniciAdi = b.Kullanici.Ad + " " + b.Kullanici.Soyad
            })
            .ToListAsync();
        }
    }
}
