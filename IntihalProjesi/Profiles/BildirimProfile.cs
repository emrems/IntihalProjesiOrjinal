using AutoMapper;
using IntihalProjesi.Dtos.Bildirim;
using IntihalProjesi.Models;

public class BildirimProfile : Profile
{
    public BildirimProfile()
    {
        // BildirimCreateDTO -> Bildirim
        CreateMap<BildirimCreateDTO, Bildirim>()
            .ForMember(dest => dest.KullaniciId, opt => opt.Ignore()); // Kullanıcı id'yi dışarıdan alacağız

        // Bildirim -> BildirimCreateDTO (Ters dönüşüm ekleniyor)
        CreateMap<Bildirim, BildirimCreateDTO>();
    }
}
