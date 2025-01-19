namespace IntihalProjesi.Dtos.IcerikDtos
{
    public class IcerikUpdateDto
    {

        public string Baslik { get; set; } // Ödev Başlığı
        public string Aciklama { get; set; } // Ödev Açıklaması
        public DateTime? BitisTarihi { get; set; } // Ödev Bitiş Tarihi
    }
}
