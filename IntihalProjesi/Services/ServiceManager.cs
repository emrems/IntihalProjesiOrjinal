using AutoMapper;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;
using IntihalProjesi.Services;
//using IntihalProjesi.SignalR;
using Microsoft.AspNetCore.SignalR;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IKullaniciService> _kullaniciService;
    private readonly Lazy<IIcerikService> _IcerikService;
    private readonly Lazy<IDosyaService> _DosyaService;
    private readonly Lazy<IBenzerlikSonucuService> _benzerlikSonucuService;
    private readonly Lazy<IBildirimService> _bildirimService;

    public ServiceManager(IRepositoryManager manager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _kullaniciService = new Lazy<IKullaniciService>(() => new KullaniciManager(manager, mapper));

        // IcerikManager için IBildirimService parametresini de iletmelisiniz
        _IcerikService = new Lazy<IIcerikService>(() => new IcerikManager(httpContextAccessor, manager, mapper));

        _DosyaService = new Lazy<IDosyaService>(() => new DosyaManager(manager));

        // Lazy olarak enjekte etme
        _benzerlikSonucuService = new Lazy<IBenzerlikSonucuService>(() => new BenzerlikSonucuManager(manager));
        _bildirimService = new Lazy<IBildirimService>(() => new BildirimManager(manager,mapper,httpContextAccessor));
    }

    public IKullaniciService KullaniciService => _kullaniciService.Value;

    public IIcerikService IcerikService => _IcerikService.Value;
    public IDosyaService DosyaService => _DosyaService.Value;

    public IBenzerlikSonucuService BenzerlikSonucuService => _benzerlikSonucuService.Value;
    public IBildirimService BildirimService => _bildirimService.Value;
}
