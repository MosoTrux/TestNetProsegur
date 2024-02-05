using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Dtos.ProductDto
{
    public class CheckOrderStockResponseDto
    {
        public List<Product> ProductsInStock { get; set; }
        public List<Product> ProductsWithStockToBeReduced { get; set; }
        public List<Product> ProductsOutOfStock { get; set; }
    }
}
