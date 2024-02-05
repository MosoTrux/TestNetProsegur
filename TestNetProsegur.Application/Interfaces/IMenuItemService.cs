using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<ServiceResponseDto<IEnumerable<MenuItem>>> GetAllAsync();
        Task<ServiceResponseDto<MenuItem>> Register(MenuItem entity);
        Task<ServiceResponseDto<MenuItem>> Update(MenuItem entity);
    }
}
