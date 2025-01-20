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
        Task<IEnumerable<Icerik>> GetAllAsync(); // Tüm içerikleri getir


    }
}
