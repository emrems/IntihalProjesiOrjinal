﻿namespace IntihalProjesi.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IKullaniciRepository KullaniciRepository { get; }
        IIcerikRepository IcerikRepository { get; }
        IDosyaRepository DosyaRepository { get; }
        IBenzerlikSonuclariRepository BenzerlikSonuclariRepository { get; }
        Task save();
    }
}
