namespace IntihalProjesi.Dtos.IcerikDtos
{
    public class IcerikReadDto
    {
        public int IcerikId { get; set; } // Ödev ID'si
        public string Baslik { get; set; } // Ödev Başlığı
        public string Aciklama { get; set; } // Ödev Açıklaması
        public DateTime OlusturmaTarihi { get; set; } // Ödev Oluşturma Tarihinp
        public DateTime? BitisTarihi { get; set; } // Ödev Bitiş Tarihi
        public string IcerikTuru { get; set; }

        // jwt kullanınca gelecek
        public string OlusturanKullanici { get; set; } // Öğretmenin adı (isteğe bağlı)
    }
}
