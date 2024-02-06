namespace TestNetProsegur.Application.Dtos.Order
{
    public class GetOrderResponseDto
    {
        public long OrderId { get; set; }
        public string Customer { get; set; }
        public string Employeed { get; set; }
        public string Province { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetOrderItemDto> OrderItems { get; set; }
    }

    public class GetOrderItemDto
    {
        public string MenuItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
