using IntihalProjesi.Models;

namespace IntihalProjesi.Services.Contracts
{
    public interface IDosyaService
    {
        Task<IEnumerable<Dosya>> GetAllDosya();
    }
}
