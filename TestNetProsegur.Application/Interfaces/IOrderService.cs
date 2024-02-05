using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Order;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponseDto<Order>> Cancel(long id);
        Task<ServiceResponseDto<Order>> GetById(long id);
        Task<ServiceResponseDto<Order>> Register(RegisterOrderDto model);
    }
}
