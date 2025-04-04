namespace IntihalProjesi.Models
{
    public class Bildirim
    {
        public int BildirimId { get; set; }
        public string Mesaj { get; set; }
        public bool Okundu { get; set; } = false;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.UtcNow;
       
        public int KullaniciId { get; set; }
       

       
        public Kullanici Kullanici { get; set; }
        
    }
}
