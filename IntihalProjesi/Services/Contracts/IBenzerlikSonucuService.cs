namespace IntihalProjesi.Services.Contracts
{
    public interface IBenzerlikSonucuService
    {
        Task<List<dynamic>> GetBenzerlikSonuclariByIcerikIdAsync(int icerikId);
    }
}
