namespace IntihalProjesi.Helpers.Contract
{
    public interface IJwtHelper
    {
        string GenerateToken(int kullaniciId, string rol);
        string GenerateRefreshToken();
        (int KullaniciId, string Rol)? DecodeToken(string token);
    }
}
