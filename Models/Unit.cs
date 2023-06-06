using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("Units")]
    public class Unit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50), Display(Name = "Підрозділ")]
        public string Number  { get; set; } = string.Empty;

        [ForeignKey("Unit"), Display(Name = "Керуючий підрозділ")]
        public Guid? ParentId { get; set; }

        [Display(Name = "Керуючий підрозділ")]
        public virtual Unit? Parent { get; set; }

        [Display(Name = "Керуючий підрозділ")]
        public string ParentNumber => Parent?.Number ?? string.Empty;

        [Display(Name = "Підпорядковані підрозділи")]
        public virtual ICollection<Unit>? SubUnits { get; set; }

        [Display(Name = "Порядок сортування")]
        public int OrderVal { get; set; } = 1;

        [Display(Name = "Бійці")]
        public virtual ICollection<User>? Users { get; set; }
    }
}
