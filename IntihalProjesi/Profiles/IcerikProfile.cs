using AutoMapper;
using IntihalProjesi.Dtos.IcerikDtos;
using IntihalProjesi.Models;

namespace IntihalProjesi.Profiles
{
    public class IcerikProfile : Profile
    {
        public IcerikProfile()
        {

            CreateMap<Icerik, IcerikReadDto>();
            CreateMap<IcerikCreateDto, Icerik>();
            CreateMap<IcerikUpdateDto, Icerik>();


            // jwt kullanınca olacak
            //CreateMap<Icerik, IcerikReadDto>()
            //    // frond endde öğrenciler ödevi oluşturan kişinin adını ve soyadını görecek
            //    .ForMember(dest => dest.OlusturanKullanici, opt => opt.MapFrom(src => src.Kullanici.Ad + " " + src.Kullanici.Soyad));
            //CreateMap<IcerikCreateDto, Icerik>();
            //CreateMap<IcerikUpdateDto, Icerik>();
        }
    }
}
