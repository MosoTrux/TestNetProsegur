namespace TestNetProsegur.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Stock { get; set; }
    }
}
