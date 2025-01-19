using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IIcerikRepository : IRepositorybase<Icerik>
    {
        // Ödevleri belirli bir öğretmene göre listeleme
        Task<IEnumerable<Icerik>> GetByTeacherIdAsync(int teacherId);

        // Bitiş tarihi geçmemiş ödevleri listeleme
        Task<IEnumerable<Icerik>> GetActiveAssignmentsAsync();

        // Belirli bir ID'ye göre ödevi alma
        Task<Icerik> GetByIdAsync(int id);
    
    }
}
