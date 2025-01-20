using System.ComponentModel.DataAnnotations;

namespace IntihalProjesi.Dtos
{
    public class KullaniciCreateDto
    {

        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        [Required]
        public string Eposta { get; set; }
        [Required]
        public string Sifre { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
