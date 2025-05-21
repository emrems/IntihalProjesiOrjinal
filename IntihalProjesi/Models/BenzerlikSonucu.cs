namespace IntihalProjesi.Models
{
    public class BenzerlikSonucu
    {
        public int SonucId { get; set; } // Primary Key
        public int? IlkDosyaId { get; set; } // Foreign Key
        public Dosya? IlkDosya { get; set; } // Navigation Property

        public int? IkinciDosyaId { get; set; } // Foreign Key
        public Dosya? IkinciDosya { get; set; } // Navigation Property

        public int? IcerikId { get; set; }
        public Icerik Icerik { get; set; } // Navigation Property

        public double BenzerlikOrani { get; set; }
    }
}
