namespace IntihalProjesi.Repositories.Contracts
{
    public interface IBenzerlikSonuclariRepository
    {
        Task<List<dynamic>> GetBenzerlikSonuclariByIcerikIdAsync(int icerikId);
    }
}
