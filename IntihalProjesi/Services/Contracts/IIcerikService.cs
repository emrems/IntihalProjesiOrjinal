using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;

namespace IntihalProjesi.Services.Contracts
{
    public interface IIcerikService
    {
        // Tüm ödevleri listelemek
        Task<IEnumerable<IcerikReadDto>> GetAllAsync();

        // Belirli bir öğretmene ait ödevleri listelemek
        Task<IEnumerable<IcerikReadDto>> GetByTeacherNameAsync(string teacherName);
        Task<IcerikReadDto> GetByIdAsync(int id);

        // Öğretmene ait ödevleri listeleme
        Task<IEnumerable<IcerikReadDto>> GetByTeacherIdAsync(int teacherId);


        // Bitiş tarihi geçmemiş ödevleri listeleme
        Task<IEnumerable<Icerik>> GetActiveAssignmentsAsync();

        // Yeni bir ödev oluşturmak
        Task<IcerikReadDto> CreateAsync(IcerikCreateDto icerikCreateDto);
        Task<IcerikDetailsDto> GetDetailsIcerik(int contentId); // İçerik detaylarını getir

        // Belirli bir ödevi güncellemek
        Task UpdateAsync(int id, IcerikUpdateDto icerikCreateDto);

        // Belirli bir ödevi silmek
        Task DeleteAsync(int id);
    }
}
