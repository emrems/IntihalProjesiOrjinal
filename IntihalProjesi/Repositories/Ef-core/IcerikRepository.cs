using IntihalProjesi.Dtos.IcerikDtos;
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


        public async Task<IEnumerable<Icerik>> GetByTeacherNameAsync(string teacherName)
        {
            return await _context
                .Icerikler
                .Include(k => k.Kullanici) // Kullanici tablosunu dahil etmemiz lazım zaten içerik tablosuna eklemişti nagivation property olarak
                .Where(k => k.Kullanici.Ad == teacherName) // Öğretmen adına göre filtrele
                .ToListAsync(); // Sorguyu çalıştır ve liste olarak döndür
        }


        public async Task<Icerik> GetByIdAsync(int id)
        {
            return await _context.Icerikler
                .Include(i => i.Kullanici) // Kullanıcı bilgilerini dahil et
                .FirstOrDefaultAsync(i => i.IcerikId == id);
        }

        public async  Task<IEnumerable<Icerik>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.Icerikler
               // .Include(i => i.Kullanici) // Kullanıcı bilgilerini dahil et detay göstermek için
                .Where(i => i.KullaniciId == teacherId)
                .ToListAsync();
        }

        public async Task<Icerik> GetDetailsIcerik(int contentId)
        {
            return await _context.Icerikler
                .Include(i => i.Kullanici)
                .FirstOrDefaultAsync(i => i.IcerikId == contentId);
        }
    }
}
