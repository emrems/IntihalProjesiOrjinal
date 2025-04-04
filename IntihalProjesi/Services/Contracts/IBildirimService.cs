using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Models;

namespace IntihalProjesi.Services.Contracts
{
    public interface IBildirimService
    {
        Task NotifyStudentAsync(int studentUserId, string message, int? contentId = null);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task<List<BildirimDTO>> ShowUnreadNotifications(int userId);
        Task<BildirimCreateDTO> CreateNotificationAsync(BildirimCreateDTO bildirimCreateDTO);
        Task<IEnumerable<BildirimDTO>> GetAllBildirimlerAsync();
    }
}
