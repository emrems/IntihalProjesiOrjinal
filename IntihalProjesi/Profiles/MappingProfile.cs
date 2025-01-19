using AutoMapper;
using IntihalProjesi.Dtos;
using IntihalProjesi.Models;

namespace IntihalProjesi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Kullanici DTO Eşleştirmeleri
            CreateMap<Kullanici, KullaniciReadDto>();//Veritabanından gelen Kullanici modelini API yanıtı olarak dönecek KullaniciReadDto'ya dönüştürmek
            CreateMap<KullaniciCreateDto, Kullanici>();//İstemciden gelen KullaniciCreateDto'yu veritabanına kaydedilecek Kullanici modeline dönüştürmek
            CreateMap<KullaniciUpdateDto, Kullanici>();//yalnızca güncelleme yapılacak alanları içerir. Örneğin, Sifre güncellenmek zorunda değildir.
        }
    }
}
