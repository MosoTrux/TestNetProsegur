namespace TestNetProsegur.Core.Entities
{
    public class Order : BaseEntity
    {
        public long CustomerId { get; set; }
        public string ProvinceCode { get; set; } 
        public bool State { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
}
