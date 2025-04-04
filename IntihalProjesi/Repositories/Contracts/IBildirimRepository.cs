using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IBildirimRepository : IRepositorybase<Bildirim>
    {
        Task AddNotificationAsync(Bildirim bildirim);
        Task<List<BildirimDTO>> GetNotificationsByUserIdAsync(int userId);
        Task<IEnumerable<BildirimDTO>> GetAllBildirimlerAsync();

    }
}
