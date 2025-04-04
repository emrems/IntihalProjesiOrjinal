using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IBenzerlikSonuclariRepository : IRepositorybase<BenzerlikSonucu>
    {
        Task<List<dynamic>> GetBenzerlikSonuclariByIcerikIdAsync(int icerikId);
    }
}
