using AutoMapper;
using IntihalProjesi.Dtos;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;

namespace IntihalProjesi.Services
{
    public class KullaniciManager : IKullaniciService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public KullaniciManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KullaniciReadDto>> GetAllAsync()
        {
            var kullanicilar = await _manager.KullaniciRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<KullaniciReadDto>>(kullanicilar);
        }

        public async Task<KullaniciReadDto> GetByIdAsync(int id)
        {
            var kullanici = await _manager.KullaniciRepository.GetByIdAsync(id);
            return _mapper.Map<KullaniciReadDto>(kullanici);
        }

        public async Task<KullaniciReadDto> CreateAsync(KullaniciCreateDto kullaniciDto)
        {
            // DTO'dan model oluşturuluyor
            var kullanici = _mapper.Map<Kullanici>(kullaniciDto);

            // Repository'ye ekleme işlemi
            await _manager.KullaniciRepository.AddAsync(kullanici);

            // Veritabanına kaydetme işlemi
            await _manager.save();

            // Modelden DTO'ya dönüşüm ve döndürme
            return _mapper.Map<KullaniciReadDto>(kullanici);
        }


        public async Task UpdateAsync(int id, KullaniciUpdateDto kullaniciDto)
        {
            var kullanici = await _manager.KullaniciRepository.GetByIdAsync(id);
            if (kullanici == null) throw new Exception("Kullanıcı bulunamadı.");

            _mapper.Map(kullaniciDto, kullanici); // Sadece gönderilen alanlar güncellenir
            _manager.KullaniciRepository.Update(kullanici);
            await _manager.save();
        }

        public async Task DeleteAsync(int id)
        {
            var kullanici = await _manager.KullaniciRepository.GetByIdAsync(id);
            if (kullanici == null) throw new Exception("Kullanıcı bulunamadı.");

            _manager.KullaniciRepository.Delete(kullanici);
            await _manager.save();
        }
    }
}
