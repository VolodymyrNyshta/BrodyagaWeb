using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrodyagaWeb.Models
{
    public class GoodImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Зображення")]
        public string FileName { get; set; } = string.Empty;

        public string ImageUrl => "\\imgs\\" + FileName;

        public virtual ICollection<GoodInStockImage>? GoodInStockImage { get; set; }

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }
    }
}
