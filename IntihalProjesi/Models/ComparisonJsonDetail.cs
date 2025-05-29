using System.ComponentModel.DataAnnotations;

namespace IntihalProjesi.Models
{
    public class ComparisonJsonDetail
    {
        [Key]
        public int DetailId { get; set; }

        public int ContentId { get; set; }

        public int FirstFileId { get; set; }

        public int SecondFileId { get; set; }

        public string JsonFilePath { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public virtual Icerik Content { get; set; }

        public virtual Dosya FirstFile { get; set; }

        public virtual Dosya SecondFile { get; set; }
    }
}
