using IntihalProjesi.Models;

namespace IntihalProjesi.Services.Contracts
{
    public interface IDosyaService
    {
        Task<IEnumerable<Dosya>> GetAllDosya();
        Task<IEnumerable<Dosya>> GetDosyaByIdAsync(int id);
        Task<Dosya> GetById(int id);
    }
}
