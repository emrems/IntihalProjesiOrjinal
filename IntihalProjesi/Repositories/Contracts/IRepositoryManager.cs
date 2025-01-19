namespace IntihalProjesi.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IKullaniciRepository KullaniciRepository { get; }
        Task save();
    }
}
