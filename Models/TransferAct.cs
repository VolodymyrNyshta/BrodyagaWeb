using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrodyagaWeb.Models
{
    [Table("TransferAct"), Display(Name = "Акти Надх./Повер. майна"),
     Comment("Передача имущества на/из склад извне - Батальйон/Волонтеры...")]
    public class TransferAct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Номер")]
        public string ActNumber { get; set; } = string.Empty;

        [Display(Name = "Коментар")]
        public string? Comment { get; set; }

        [Display(Name = "Майно")]
        public virtual IEnumerable<GoodInStock>? GoodsInStock { get; set; }

        [ForeignKey(nameof(DictExtSrc)), Display(Name = "Зовн. джерело")]
        public Guid DictExtSrcId { get; set; }

        public DictExtSrc? DictExtSrc { get; set; }

        [DefaultValue(true), Required, Display(Name = "Надх./Повер.")]
        public bool IsGiveOut { get; set; } = true;

        [Column(TypeName = "SMALLDATETIME"), DataType(DataType.Date),
         Required, Display(Name = "Дата акту")]
        public DateTime ActDate { get; set; } = DateTime.Now;

        [Column(TypeName = "SMALLDATETIME"), DataType(DataType.Date),
         Display(Name = "Зареєстровано"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegisterDate { get; set; }
        
    }
}
