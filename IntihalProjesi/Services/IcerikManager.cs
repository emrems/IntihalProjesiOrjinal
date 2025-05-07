using AutoMapper;
using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace IntihalProjesi.Services
{
    public class IcerikManager : IIcerikService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IBildirimService _bildirimService;

        public IcerikManager(IHttpContextAccessor httpContextAccessor,IRepositoryManager repository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IcerikReadDto>> GetAllAsync()
        {
            var icerikler = await _repository.IcerikRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IcerikReadDto>>(icerikler);
        }

        public async Task<IEnumerable<IcerikReadDto>> GetByTeacherNameAsync(string teacherName)
        {
            var icerikler = await _repository.IcerikRepository.GetByTeacherNameAsync(teacherName);
            return _mapper.Map<IEnumerable<IcerikReadDto>>(icerikler);
        }

        public async Task<IcerikReadDto> CreateAsync(IcerikCreateDto icerikCreateDto)
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim))
            {
                throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı. Yetkilendirme başarısız.");
            }

            var kullaniciId = int.Parse(idClaim);

            var icerik = _mapper.Map<Icerik>(icerikCreateDto);
            icerik.KullaniciId = kullaniciId;

            await _repository.IcerikRepository.AddAsync(icerik);
            await _repository.save();

            return _mapper.Map<IcerikReadDto>(icerik);
        }


        public async Task UpdateAsync(int id, IcerikUpdateDto icerikCreateDto)
        {
            var icerik = await _repository.IcerikRepository.GetByIdAsync(id);
            if (icerik == null) throw new Exception("Ödev bulunamadı.");

            _mapper.Map(icerikCreateDto, icerik);
            _repository.IcerikRepository.Update(icerik);
            await _repository.save();
        }

        public async Task DeleteAsync(int id)
        {
            var icerik = await _repository.IcerikRepository.GetByIdAsync(id);
            if (icerik == null) throw new Exception("Ödev bulunamadı.");

            _repository.IcerikRepository.Delete(icerik);
            await _repository.save();
        }

        public async Task<IcerikReadDto> GetByIdAsync(int id)
        {
            var icerik = await _repository.IcerikRepository.GetByIdAsync(id);
            if (icerik == null) return null;
            return _mapper.Map<IcerikReadDto>(icerik);
        }

        public async  Task<IEnumerable<Icerik>> GetActiveAssignmentsAsync()
        {
            return await _repository.IcerikRepository.GetActiveAssignmentsAsync();

        }

        public async Task<IEnumerable<IcerikReadDto>> GetByTeacherIdAsync(int teacherId)
        {
            var assigments = await _repository.IcerikRepository.GetByTeacherIdAsync(teacherId);
            if(assigments == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<IcerikReadDto>>(assigments);
        }

        public async Task<IcerikDetailsDto> GetDetailsIcerik(int contentId)
        {
            var icerik = await _repository.IcerikRepository.GetDetailsIcerik(contentId);
            if (icerik == null)
            {
                throw new Exception("İçerik bulunamadı.");
            }

            return _mapper.Map<IcerikDetailsDto>(icerik);
        }   
    }
}
