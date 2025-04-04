namespace IntihalProjesi.Services.Contracts
{
    public interface IServiceManager
    {
        IKullaniciService KullaniciService { get; }
        IIcerikService IcerikService { get; }
        IDosyaService DosyaService { get; }
        IBenzerlikSonucuService BenzerlikSonucuService { get; }
        IBildirimService BildirimService { get; }
    }
}
