using System.Security.Cryptography;

namespace IntihalProjesi.Models
{
    public class Icerik
    {

        public int IcerikId { get; set; } // Primary Key
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public int KullaniciId { get; set; } // Foreign Key
        public Kullanici Kullanici { get; set; } // Navigation Property

        // Navigation Properties
        public ICollection<Dosya> Dosyalar { get; set; } // İçeriğe bağlı dosyalar
    }
}
