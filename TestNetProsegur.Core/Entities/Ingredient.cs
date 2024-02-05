namespace TestNetProsegur.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public long IdMenuItem { get; set; }
        public long IdProduct { get; set; }
        public decimal Quantity { get; set; }

        public virtual MenuItem IdMenuItemNavigation { get; set; }
    }
}
