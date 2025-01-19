using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class IcerikRepository : RepositoryBase<Icerik> ,IIcerikRepository
    {
        
        public IcerikRepository(OrjinalIntihalDbContext context) : base(context) 
        {
            
        }
        public async Task<IEnumerable<Icerik>> GetAllAsync()
        {
            return await _context.Icerikler
                .Include(i => i.Kullanici) // Kullanıcı bilgilerini dahil et
                .ToListAsync();
        }

        public async Task<IEnumerable<Icerik>> GetActiveAssignmentsAsync()
        {
            return await _context
                    .Icerikler
                    .Where(k =>
                        (k.BitisTarihi == null || k.BitisTarihi > DateTime.UtcNow) && // Henüz bitmemiş
                        (k.OlusturmaTarihi <= DateTime.UtcNow)) // Başlangıç tarihi geçmiş
                    .ToListAsync();
        }


        public async Task<IEnumerable<Icerik>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context
                    .Icerikler
                    .Where(k=>k.KullaniciId == teacherId)
                    .ToListAsync();

        }


        public async Task<Icerik> GetByIdAsync(int id)
        {
            return await _context.Icerikler
               // .Include(i => i.Kullanici) // Kullanıcı bilgilerini dahil et
                .FirstOrDefaultAsync(i => i.IcerikId == id);
        }


    }
}
