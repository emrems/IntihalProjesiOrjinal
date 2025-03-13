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

        public async Task<Dosya> GetById(int id)
        {
            var dosya = await _manager.DosyaRepository.GetById(id);
            if (dosya == null)
            {
                throw new Exception("Dosya bulunamadı.");
            }
            return dosya;
        }

        public async Task<IEnumerable<Dosya>> GetDosyaByIdAsync(int id)
        {
            var dosyalar = await _manager.DosyaRepository.GetDosyaByIdAsync(id);
            if (dosyalar == null)
            {
                throw new Exception("Dosya bulunamadı.");
            }
            return dosyalar;
        }
    }
}
