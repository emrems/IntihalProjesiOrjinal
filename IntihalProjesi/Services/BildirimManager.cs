using AutoMapper;
using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Repositories.Ef_core;
using IntihalProjesi.Services.Contracts;
//using IntihalProjesi.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace IntihalProjesi.Services
{
    public class BildirimManager : IBildirimService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public BildirimManager(IRepositoryManager manager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var bildirim = await _manager.BildirimRepository.GetByIdAsync(notificationId);
            if (bildirim != null)
            {
                bildirim.Okundu = true;
                _manager.BildirimRepository.Update(bildirim);
                await _manager.save();
            }
        }

        public async Task NotifyStudentAsync(int studentUserId, string message, int? contentId = null)
        {
            // Bildirimi veritabanına kaydet
            var bildirim = new Bildirim
            {
                KullaniciId = studentUserId,
                Mesaj = message,
            };

            await _manager.BildirimRepository.AddNotificationAsync(bildirim);
            await _manager.save();

            
        }
        public async Task<List<BildirimDTO>> ShowUnreadNotifications(int userId)
        { 
            var user = await _manager.KullaniciRepository.GetByIdAsync(userId);
            if (user != null)
            {
                return await _manager.BildirimRepository.GetNotificationsByUserIdAsync(userId);
            }
            return new List<BildirimDTO>();
        }

        public async Task<BildirimCreateDTO> CreateNotificationAsync(BildirimCreateDTO bildirimCreateDTO)
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı. Yetkilendirme başarısız.");
            }

            var kullaniciId = int.Parse(idClaim);

            // DTO'dan Model'e Dönüştürme
            var bildirim = _mapper.Map<Bildirim>(bildirimCreateDTO);
            bildirim.KullaniciId = kullaniciId;

          

            await _manager.BildirimRepository.AddAsync(bildirim);
            await _manager.save();

            // Bildirim'i tekrar DTO'ya dönüştürme
            return _mapper.Map<BildirimCreateDTO>(bildirim); 
        }

        public async Task<IEnumerable<BildirimDTO>> GetAllBildirimlerAsync()
        {
            return await _manager.BildirimRepository.GetAllBildirimlerAsync();
        }
    }
}
