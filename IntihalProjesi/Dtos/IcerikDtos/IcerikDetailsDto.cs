namespace IntihalProjesi.Dtos.IcerikDtos
{
    public class IcerikDetailsDto
    {
        public int IcerikId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string IcerikTuru { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public string KullaniciAd { get; set; }
        public string KullaniciSoyad { get; set; }
        public string KullaniciEposta { get; set; }
    }
}
