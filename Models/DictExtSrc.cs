using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BrodyagaWeb.Models
{
    [Table("DictExtSrc"), Index(nameof(Value), IsUnique = true), Display(Name = "Зовн. джерело")]
    public class DictExtSrc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Зовн. джерело")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }
    }
}
