namespace TestNetProsegur.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public long IdOrder { get; set; }
        public long IdMenuItem { get; set; }
        public int Quantity { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual MenuItem IdMenuItemNavigation { get; set; }
    }
}
