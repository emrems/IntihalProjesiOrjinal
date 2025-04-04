using IntihalProjesi.Repositories.Config;
using IntihalProjesi.Repositories.Contracts;

namespace IntihalProjesi.Repositories.Ef_core
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly OrjinalIntihalDbContext _context;
        private readonly Lazy<IKullaniciRepository> _kullaniciRepository;
        private readonly Lazy<IIcerikRepository> _IcerikRepository;
        private readonly Lazy<IDosyaRepository> _dosyaRepository;
        private readonly Lazy<IBenzerlikSonuclariRepository> _benzerlikSonuclari;
       private readonly Lazy<IBildirimRepository> _bildirimRepository;


        public RepositoryManager(OrjinalIntihalDbContext context,IBenzerlikSonuclariRepository benzerlikSonuclari)
        {
            _context = context;
            _kullaniciRepository = new Lazy<IKullaniciRepository>(() => new KullaniciRepository(context));
            _IcerikRepository = new Lazy<IIcerikRepository>(() => new IcerikRepository(context));
            _dosyaRepository = new Lazy<IDosyaRepository>(() => new DosyaRepository(context));
            _benzerlikSonuclari = new Lazy<IBenzerlikSonuclariRepository>(() => new BenzerlikSonuclariRepository(context));
            _bildirimRepository = new Lazy<IBildirimRepository>(() => new BildirimRepository(context));
        }

        // KullaniciRepository çağırdığımda KullaniciRepository sayfasına gidip ordaki özellikleri kullanabilirim. 
        public IKullaniciRepository KullaniciRepository => _kullaniciRepository.Value;// sadece KullaniciRepository dediğimde newlencek

        public IIcerikRepository IcerikRepository => _IcerikRepository.Value;

        public IDosyaRepository DosyaRepository => _dosyaRepository.Value;

        public IBenzerlikSonuclariRepository BenzerlikSonuclariRepository => _benzerlikSonuclari.Value;
        public IBildirimRepository BildirimRepository => _bildirimRepository.Value;

        public async Task save()
        {
            _context.SaveChanges();
        }
    }
}
