namespace IntihalProjesi.Dtos.IcerikDtos
{
    public class IcerikCreateDto
    {
        public string Baslik { get; set; } // Ödev Başlığı
        public string Aciklama { get; set; } // Ödev Açıklaması
        public DateTime? OlusturmaTarihi { get; set; } = DateTime.UtcNow; // Varsayılan olarak oluşturulma tarihi
        public DateTime? BitisTarihi { get; set; } // Ödev Bitiş Tarihi
        public string IcerikTuru { get; set; } 


        // jwt kullanınca bu kısım gelecek
        //public int KullaniciId { get; set; } // Ödevi oluşturan öğretmen


    }
}

