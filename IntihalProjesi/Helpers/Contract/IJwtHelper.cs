namespace IntihalProjesi.Helpers.Contract
{
    public interface IJwtHelper
    {
        string GenerateToken(int kullaniciId, string rol);
    }
}
