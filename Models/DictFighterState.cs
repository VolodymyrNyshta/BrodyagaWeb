using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("DictFigterState"), Index(nameof(Value), IsUnique = true)]
    public class DictFighterState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(10), Display(Name = "Статус бійця")]
        public string Value { get; set; }

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }

        public DictFighterState(Guid id, string value, string? comment)
        {
            Id = id;
            Value = value;
            Comment = comment;
        }

        public DictFighterState() : this(Guid.Empty, string.Empty, string.Empty) { }
    }
}
