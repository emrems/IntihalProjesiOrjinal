using AutoMapper;
using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;

namespace IntihalProjesi.Services
{
    public class IcerikManager : IIcerikService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public IcerikManager(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IcerikReadDto>> GetAllAsync()
        {
            var icerikler = await _repository.IcerikRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IcerikReadDto>>(icerikler);
        }

        public async Task<IEnumerable<IcerikReadDto>> GetByTeacherIdAsync(int teacherId)
        {
            var icerikler = await _repository.IcerikRepository.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<IcerikReadDto>>(icerikler);
        }

        public async Task<IcerikReadDto> CreateAsync(IcerikCreateDto icerikCreateDto)
        {
            var icerik = _mapper.Map<Icerik>(icerikCreateDto);

            // Şimdilik öğretmenin ID'si sabit (JWT eklenince değiştirilecek)
            icerik.KullaniciId = 5;

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
    }
}
