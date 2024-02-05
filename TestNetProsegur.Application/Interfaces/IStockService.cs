using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.ProductDto;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IStockService
    {
        ServiceResponseDto<CheckOrderStockResponseDto> CheckOrderStock(List<OrderItem> model);
        Task<ServiceResponseDto<List<Product>>> AddStock(List<AddStockDto> model);   
    }
}
