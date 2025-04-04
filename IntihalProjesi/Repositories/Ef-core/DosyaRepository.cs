using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class DosyaRepository : RepositoryBase<Dosya> , IDosyaRepository
    {
        public DosyaRepository( OrjinalIntihalDbContext context) :base(context)
        {
            
        }
        public async  Task<IEnumerable<Dosya>> GetAllDosyaAsync()
        {
            return await _context.Set<Dosya>().ToListAsync();
        }

        public async Task<Dosya> GetById(int id)
        {
            return await _context.Set<Dosya>().FindAsync(id);
        }

        public async Task<IEnumerable<Dosya>> GetDosyaByIdAsync(int id)
        {
            return await _context.Set<Dosya>()
                                 .Where(x => x.IcerikId == id)
                                  .Include(x => x.Kullanici)  // Kullanıcı bilgilerini ekle
                               // .Include(x => x.Icerik)    // İçerik bilgilerini ekle
                                 .ToListAsync();
        }

    }
}
