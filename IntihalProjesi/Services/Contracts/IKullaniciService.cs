using IntihalProjesi.Dtos;

namespace IntihalProjesi.Services.Contracts
{
    public interface IKullaniciService
    {
        Task<IEnumerable<KullaniciReadDto>> GetAllAsync(); // Tüm kullanıcıları listele
        Task<KullaniciReadDto> GetByIdAsync(int id);       // ID'ye göre kullanıcı getir
        Task<KullaniciReadDto> CreateAsync(KullaniciCreateDto kullaniciDto); // Yeni kullanıcı oluştur
        Task UpdateAsync(int id, KullaniciUpdateDto kullaniciDto);          // Kullanıcı güncelle
        Task DeleteAsync(int id);
    }
}
