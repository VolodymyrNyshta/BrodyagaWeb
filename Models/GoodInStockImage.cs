using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrodyagaWeb.Models
{
    public class GoodInStockImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(GoodImage)), Display(Name = "Зображення")]
        public Guid GoodImageId { get; set; }

        [Display(Name = "Зображення")]
        public GoodImage? GoodImage { get; set; }

        [ForeignKey(nameof(GoodInStock)), Display(Name = "Майно")]
        public Guid GoodInStockId { get; set; }

        [Display(Name = "Майно")]
        public GoodInStock? GoodInStock { get; set; }
    }
}
