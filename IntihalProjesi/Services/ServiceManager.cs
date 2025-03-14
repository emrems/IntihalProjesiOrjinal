﻿using AutoMapper;
using IntihalProjesi.Repositories.Contracts;
using IntihalProjesi.Services.Contracts;

namespace IntihalProjesi.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IKullaniciService> _kullaniciService;
        private readonly Lazy<IIcerikService> _IcerikService;
        private readonly Lazy<IDosyaService> _DosyaService;



        public ServiceManager(IRepositoryManager manager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _kullaniciService = new Lazy<IKullaniciService>(() => new KullaniciManager(manager, mapper));
            _IcerikService = new Lazy<IIcerikService>(()=> new IcerikManager(httpContextAccessor,manager, mapper));
            _DosyaService = new Lazy<IDosyaService>(() => new DosyaManager(manager));
            
        }

        public IKullaniciService KullaniciService => _kullaniciService.Value;

        public IIcerikService IcerikService => _IcerikService.Value;
        public IDosyaService DosyaService => _DosyaService.Value;

    }
}
