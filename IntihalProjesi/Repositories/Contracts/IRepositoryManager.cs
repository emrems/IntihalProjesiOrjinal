namespace IntihalProjesi.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IKullaniciRepository KullaniciRepository { get; }
        IIcerikRepository IcerikRepository { get; }
        Task save();
    }
}
