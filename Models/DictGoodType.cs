using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("DictGoodType"), Index(nameof(Value), IsUnique = true)]
    public class DictGoodType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Вид майна")]
        public string Value { get; set; }

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }

        //public virtual ICollection<DictGood>? Goods { get; set; }

        public DictGoodType(Guid id, string value, string? comment)
        {
            Id = id;
            Value = value;
            Comment = comment;
        }

        public DictGoodType() : this(Guid.Empty, string.Empty, string.Empty) { }
    }
}
