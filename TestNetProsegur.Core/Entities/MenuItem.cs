namespace TestNetProsegur.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
