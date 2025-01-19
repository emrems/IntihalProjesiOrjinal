using IntihalProjesi.Models;

namespace IntihalProjesi.Repositories.Contracts
{
    public interface IKullaniciRepository : IRepositorybase<Kullanici>
    {
        Task<Kullanici> GetByEmail(string email);
    }
}
