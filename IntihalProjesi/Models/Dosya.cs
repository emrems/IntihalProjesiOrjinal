namespace IntihalProjesi.Models
{
    public class Dosya
    {

        public int DosyaId { get; set; } // Primary Key
        public string? CleanedPath { get; set; } // Cleaned File Path (Temizlenmiş dosyanın yolu)


        public int? KullaniciId { get; set; } // Foreign Key
        public Kullanici? Kullanici { get; set; } // Navigation Property

        public int? IcerikId { get; set; } // Foreign Key
        public Icerik? Icerik { get; set; } // Navigation Property
        public virtual ICollection<ComparisonJsonDetail> FirstFileComparisons { get; set; } = new List<ComparisonJsonDetail>();
        public virtual ICollection<ComparisonJsonDetail> SecondFileComparisons { get; set; } = new List<ComparisonJsonDetail>();
    }
}
