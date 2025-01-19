using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly OrjinalIntihalDbContext _context;
        private readonly Lazy<IKullaniciRepository> _kullaniciRepository;
        

        public RepositoryManager(OrjinalIntihalDbContext context)
        {
            _context = context;
            _kullaniciRepository = new Lazy<IKullaniciRepository>(() => new KullaniciRepository(context));
            
        }

        // KullaniciRepository çağırdığımda KullaniciRepository sayfasına gidip ordaki özellikleri kullanabilirim. 
        public IKullaniciRepository KullaniciRepository => _kullaniciRepository.Value;// sadece KullaniciRepository dediğimde newlencek

        

        public async Task save()
        {
            _context.SaveChanges();
        }
    }
}
