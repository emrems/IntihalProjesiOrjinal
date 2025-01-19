using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly OrjinalIntihalDbContext _context;
        private readonly Lazy<IKullaniciRepository> _kullaniciRepository;
        private readonly Lazy<IIcerikRepository> _IcerikRepository;


        public RepositoryManager(OrjinalIntihalDbContext context)
        {
            _context = context;
            _kullaniciRepository = new Lazy<IKullaniciRepository>(() => new KullaniciRepository(context));
            _IcerikRepository = new Lazy<IIcerikRepository>(() => new IcerikRepository(context));
            
        }

        // KullaniciRepository çağırdığımda KullaniciRepository sayfasına gidip ordaki özellikleri kullanabilirim. 
        public IKullaniciRepository KullaniciRepository => _kullaniciRepository.Value;// sadece KullaniciRepository dediğimde newlencek

        public IIcerikRepository IcerikRepository => _IcerikRepository.Value;

        public async Task save()
        {
            _context.SaveChanges();
        }
    }
}
