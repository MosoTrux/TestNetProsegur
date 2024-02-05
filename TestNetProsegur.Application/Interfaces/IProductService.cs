using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponseDto<Product>> Add(Product entity);
        Task<ServiceResponseDto<string>> Delete(long id);
        Task<ServiceResponseDto<IEnumerable<Product>>> GetAllAsync();
        ServiceResponseDto<IEnumerable<Product>> GetAll();
        Task<ServiceResponseDto<Product>> GetById(long id);
        Task<ServiceResponseDto<Product>> Update(Product entity);
    }
}
