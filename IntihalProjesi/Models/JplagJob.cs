using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace IntihalProjesi.Models
{
    public class JplagJob
    {
        public JplagJob()
        {
            CreatedAt = DateTime.UtcNow;
            Status = "PENDING";          
        }
        public string JobId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } 
        public string ZipPath { get; set; }
  
        public int IcerikId { get; set; }
        public Icerik? Icerik { get; set; }
    }
}
