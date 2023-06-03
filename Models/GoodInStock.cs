using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel;

namespace BrodyagaWeb.Models
{
    [Table("GoodsInStock"), Display(Name = "Майно"),
     Comment("Имущество на складе/у бойцов")]
    public class GoodInStock
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DefaultValue(1), Required, Display(Name = "Кількість")]
        public int Cnt { get; set; } = 1;

        [ForeignKey(nameof(DictSizeId)), Display(Name = "Розмір")]
        public Guid DictSizeId { get; set; }

        [Display(Name = "Розмір")]
        public virtual DictSize? DictSize { get; set; }
        public string GoodSize => DictSize != null ? DictSize.Value : string.Empty;

        [ForeignKey(nameof(DictGoods)), Display(Name = "Майно")]
        public Guid DictGoodsId { get; set; }
        [Display(Name = "Майно")]
        public virtual DictGoods? DictGoods { get; set; }

        [Display(Name = "Майно")]
        public string GoodValue => DictGoods != null ? DictGoods.Value : string.Empty;
        [Display(Name = "Одиниця вимірювання")]
        public string GoodMeasure => DictGoods != null ? DictGoods.GoodMeasure : string.Empty;

        //public string DictGoodsValue => DictGoods?.Value ?? string.Empty;
/*
        [ForeignKey(nameof(DictMeasure)), Display(Name = "Одиниця вимірювання")]
        public Guid DictMeasureId { get; set; }
        [Display(Name = "Одиниця вимірювання")]
        public virtual DictMeasure? DictMeasure { get; set; }
*/
        [Column(TypeName = "money"), Display(Name = "Вартість")]
        public virtual decimal? Price { get; set; }

        [ForeignKey(nameof(TransferAct)), Display(Name = "Акт")]
        public Guid TransferActId { get; set; }

        [Display(Name = "Акт")]
        public virtual TransferAct? TransferAct { get; set; }
        [Display(Name = "Акт")]
        public string GoodTransferAct => TransferAct != null ? TransferAct.ActNumber : string.Empty;

        [Display(Name = "Коментар")]
        public string? Comment { get; set; } = string.Empty;

        public virtual IEnumerable<GoodInStockImage>? Images { get; set; }
    }
}
