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
    }
}
