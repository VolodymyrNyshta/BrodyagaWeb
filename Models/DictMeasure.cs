using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("DictMeasure"), Comment("Одиниці вимірювання")]
    public class DictMeasure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Одиниця вимірювання")]
        public string Value { get; set; }

        [StringLength(50), Display(Name = "Опис")]
        public string? Comment { get; set; }
        public DictMeasure(Guid id, string value, string? comment)
        {
            Id = id;
            Value = value;
            Comment = comment;
        }

        public DictMeasure() : this(Guid.Empty, string.Empty, string.Empty) { }
    }
}
