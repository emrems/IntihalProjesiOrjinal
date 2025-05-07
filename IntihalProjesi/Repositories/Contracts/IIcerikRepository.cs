using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IIcerikRepository : IRepositorybase<Icerik>
    {
        // Ödevleri belirli bir öğretmene göre listeleme
        Task<IEnumerable<Icerik>> GetByTeacherNameAsync(string teacherName);

        // Bitiş tarihi geçmemiş ödevleri listeleme
        Task<IEnumerable<Icerik>> GetActiveAssignmentsAsync();

        // Belirli bir ID'ye göre ödevi alma
        Task<Icerik> GetByIdAsync(int id);

        //belli öğrtemenId'ye göre ödevleri getir
        Task<IEnumerable<Icerik>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Icerik>> GetAllAsync(); // Tüm içerikleri getir

        Task<Icerik> GetDetailsIcerik(int contentId); // İçerik detaylarını getir


    }
}
