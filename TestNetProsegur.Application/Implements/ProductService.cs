using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.ProductDto;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;

namespace TestNetProsegur.Application.Implements
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productoRepository;

        public ProductService(IRepository<Product> productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ServiceResponseDto<Product>> Add(Product entity)
        {
            var response = new ServiceResponseDto<Product>();
            try
            {
                await _productoRepository.Add(entity);
                await _productoRepository.SaveChangesAsync();
                response.Data = entity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo grabar el item. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<string>> Delete(long id)
        {
            var response = new ServiceResponseDto<string>();
            try
            {
                var currentEntity = await _productoRepository.GetById(id);
                if (currentEntity == null)
                {
                    throw new Exception("El item no existe.");
                }

                await _productoRepository.Delete(id);
                await _productoRepository.SaveChangesAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo eliminar el item. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<IEnumerable<Product>>> GetAllAsync()
        {
            var response = new ServiceResponseDto<IEnumerable<Product>>();
            try
            {
                var queryList = await _productoRepository.GetAll().ToListAsync().ConfigureAwait(false);
                response.Data = queryList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo obtener la lista. Exception: {ex.Message}");
            }
            return response;
        }

        public ServiceResponseDto<IEnumerable<Product>> GetAll()
        {
            var response = new ServiceResponseDto<IEnumerable<Product>>();
            try
            {
                var queryList = _productoRepository.GetAll();
                response.Data = queryList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo obtener la lista. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<Product>> GetById(long id)
        {
            var response = new ServiceResponseDto<Product>();
            try
            {
                var item = await _productoRepository.GetById(id);
                response.Data = item;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo obtener el item. Exception: {ex.Message}");
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponseDto<Product>> Update(Product entity)
        {
            var response = new ServiceResponseDto<Product>();
            try
            {
                var currentEntity = await _productoRepository.GetById(entity.Id);
                if (currentEntity == null)
                {
                    throw new Exception("El item no existe.");
                }

                currentEntity.Name = entity.Name;

                _productoRepository.Update(currentEntity);
                await _productoRepository.SaveChangesAsync();
                response.Data = currentEntity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo actualizar el item. Exception: {ex.Message}");
            }
            return response;
        }
    }
}
