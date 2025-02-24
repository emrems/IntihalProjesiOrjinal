using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IDosyaRepository
    {
        Task<IEnumerable<Dosya>> GetAllDosyaAsync();
    }
}
