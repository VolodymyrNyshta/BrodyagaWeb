using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("DictGoods"), Index(nameof(Value), IsUnique = true), Display(Name = "Майно")]
    public class DictGoods
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Майно")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }

        [ForeignKey(nameof(DictGoodType)), Display(Name = "Вид майна")]
        public Guid DictGoodTypeId { get; set; }

        [Display(Name = "Вид майна")]
        public virtual DictGoodType? DictGoodType { get; set; }
        public string GoodType => DictGoodType != null ? DictGoodType.Value : string.Empty;

        [ForeignKey(nameof(DictMeasure)), Display(Name = "Одиниця вимірювання")]
        public Guid DictMeasureId { get; set; }
        [Display(Name = "Одиниця вимірювання")]
        public virtual DictMeasure? DictMeasure { get; set; }
        [Display(Name = "Одиниця вимірювання")]
        public string GoodMeasure => DictMeasure != null ? DictMeasure.Value : string.Empty;
    }
}
