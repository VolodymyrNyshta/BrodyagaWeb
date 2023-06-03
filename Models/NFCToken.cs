namespace BrodyagaWeb.Models
{
    public enum TSectionId
    {
        None, Fighter, GoodInStock, TransferAct
    }

    public class NFCToken
    {
        public Guid Id { get; set; } = default;
        public TSectionId SectionId { get; set; } = TSectionId.None;
    }
}
