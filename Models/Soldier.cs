using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("Soldiers")]
    public class Soldier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Прізвище")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50), Display(Name = "Ім'я")]
        public string? MidleName { get; set; }

        [StringLength(50), Display(Name = "По батькові")]
        public string? LastName { get; set; }

        [ForeignKey("Units"), Display(Name = "Підрозділ")]
        public Guid UnitId { get; set; }

        [Display(Name = "Підрозділ")]
        public virtual Unit? Unit { get; set; }

        [Display(Name = "Підрозділ")]
        public string UnitNumber => Unit?.Number ?? string.Empty;

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
