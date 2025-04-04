namespace IntihalProjesi.Dtos.Bildirim
{
    public class BildirimCreateDTO
    {
        public string Mesaj { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;

    }
}
