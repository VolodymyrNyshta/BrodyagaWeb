//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }

        [ForeignKey(nameof(Soldier))]
        public Guid FightersId { get; set; }
        [ForeignKey(nameof(DictGoods))]
        public Guid DictGoodsId { get; set; }

        //public virtual Fighter? Fighter { get; set; }
        //public virtual DictGood? Good { get; set; }

        /*
        public Order()
        {
            Fighter = new Fighter();
            Good = new Good();
        }
        */
    }
}
