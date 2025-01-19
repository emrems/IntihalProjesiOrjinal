using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class KullaniciRepository : RepositoryBase<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(OrjinalIntihalDbContext context) : base(context)
        {

        }

        public async Task<Kullanici> GetByEmail(string email)
        {
            return await _context.Set<Kullanici>().FirstOrDefaultAsync(k => k.Eposta == email);


        }
    }
}
