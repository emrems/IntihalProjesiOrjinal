namespace IntihalProjesi.Dtos.Bildirim
{
    public class BildirimDTO
    {
       // public bool Okundu { get; set; } = false;
        public int BildirimId { get; set; }
        public string Mesaj { get; set; }
        public DateTime OlusturmaTarihi { get; set; }= DateTime.UtcNow;
        public string KullaniciAdi { get; set; }
    }
}
