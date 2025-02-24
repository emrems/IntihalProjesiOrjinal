using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IntihalProjesi.Services
{
    public class DosyaManager : IDosyaService
    {
        private readonly IRepositoryManager _manager;

        public DosyaManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IEnumerable<Dosya>> GetAllDosya()
        {
             var dosylar = await _manager.DosyaRepository.GetAllDosyaAsync();
             if(dosylar == null)
            {

                throw new Exception("Dosyalar bulunamadı.");
            }
            return dosylar;
        }
    }
}
