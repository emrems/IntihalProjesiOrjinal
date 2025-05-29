using System.Security.Cryptography;

namespace IntihalProjesi.Models
{
    public class Icerik
    {

        public int IcerikId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string IcerikTuru { get; set; } 
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public int KullaniciId { get; set; } // Foreign Key
        public Kullanici Kullanici { get; set; } // Navigation Property

       
        public ICollection<Dosya> Dosyalar { get; set; } // İçeriğe bağlı dosyalar
        public ICollection<Bildirim> Bildirimler { get; set; } = new List<Bildirim>();
        public ICollection<JplagJob> JplagJobs { get; set; } = new List<JplagJob>();
        public virtual ICollection<ComparisonJsonDetail> ComparisonJsonDetails { get; set; } = new List<ComparisonJsonDetail>();
    }
}
