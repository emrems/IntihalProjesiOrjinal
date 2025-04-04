namespace IntihalProjesi.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; } // Primary Key
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string Rol { get; set; }

        // Navigation Properties
        public ICollection<Icerik> Icerikler { get; set; } // Kullanıcının oluşturduğu içerikler
        public ICollection<Bildirim> Bildirimler { get; set; } = new List<Bildirim>();
    }
}
