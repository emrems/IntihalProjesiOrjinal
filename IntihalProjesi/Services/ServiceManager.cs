using AutoMapper;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;

namespace IntihalProjesi.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IKullaniciService> _kullaniciService;
        

        public ServiceManager(IRepositoryManager manager, IMapper mapper)
        {
            _kullaniciService = new Lazy<IKullaniciService>(() => new KullaniciManager(manager, mapper));
            
        }

        public IKullaniciService KullaniciService => _kullaniciService.Value;

        
    }
}
