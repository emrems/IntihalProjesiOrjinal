using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;

namespace IntihalProjesi.Services
{
    public class BenzerlikSonucuManager : IBenzerlikSonucuService
    {
        private readonly IRepositoryManager _repository;

        public BenzerlikSonucuManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<List<dynamic>> GetBenzerlikSonuclariByIcerikIdAsync(int icerikId)
        {
            return await _repository.BenzerlikSonuclariRepository.GetBenzerlikSonuclariByIcerikIdAsync(icerikId);
        }
    }
}
