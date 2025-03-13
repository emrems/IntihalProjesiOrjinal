using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IDosyaRepository
    {
        Task<IEnumerable<Dosya>> GetAllDosyaAsync();
        Task<IEnumerable<Dosya>> GetDosyaByIdAsync(int id);

        Task<Dosya> GetById(int id);
    }
}
