using AutoMapper;
using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;

namespace IntihalProjesi.Profiles
{
    public class IcerikProfile : Profile
    {
        public IcerikProfile()
        {
            // Kullanıcı bilgilerini güvenli şekilde eşleme
            CreateMap<Icerik, IcerikReadDto>()
                .ForMember(dest => dest.OlusturanKullanici,
                    opt => opt.MapFrom(src => src.Kullanici != null
                        ? src.Kullanici.Ad + " " + src.Kullanici.Soyad
                        : "Bilinmeyen Kullanıcı"));

            CreateMap<IcerikCreateDto, Icerik>();
            CreateMap<IcerikUpdateDto, Icerik>();
            CreateMap<Icerik, IcerikDetailsDto>()
            .ForMember(dest => dest.KullaniciAd,
                opt => opt.MapFrom(src => src.Kullanici.Ad))
            .ForMember(dest => dest.KullaniciSoyad,
                opt => opt.MapFrom(src => src.Kullanici.Soyad))
            .ForMember(dest => dest.KullaniciEposta,
                opt => opt.MapFrom(src => src.Kullanici.Eposta));

        }
    }
}
