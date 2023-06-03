using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrodyagaWeb.Models
{
    [Table("DictSize"), Display(Name = "Розмір"),
     Comment("Розміри майна S,M,41,42,56,57...")]
    public class DictSize
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Розмір")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }
        public virtual ICollection<GoodInStock>? GoodInStocks { get; set; }
    }
}
