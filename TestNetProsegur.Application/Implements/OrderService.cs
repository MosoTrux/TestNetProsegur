using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Order;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestNetProsegur.Application.Implements
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepository;
        private IStockService _stockService;

        public OrderService(IUnitOfWork unitOfWork, 
            IRepository<Order> orderRepository,
            IStockService stockService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _stockService = stockService;
        }
        public async Task<ServiceResponseDto<Order>> Cancel(long id)
        {
            var response = new ServiceResponseDto<Order>();
            try
            {
                var currentEntity = await _orderRepository.GetById(id);
                if (currentEntity == null)
                {
                    throw new Exception("El item no existe.");
                }

                currentEntity.State = false;

                _orderRepository.Update(currentEntity);
                await _orderRepository.SaveChangesAsync();
                response.Data = currentEntity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo cancelar el item. Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<ServiceResponseDto<Order>> GetById(long id)
        {
            var response = new ServiceResponseDto<Order>();
            try
            {
                var item = await _orderRepository
                    .GetBy(order => order.Id == id)
                    .Include(d=>d.OrderItems)
                    .FirstOrDefaultAsync();

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

        public async Task<ServiceResponseDto<Order>> Register(RegisterOrderDto model)
        {
            var response = new ServiceResponseDto<Order>();
            try
            {
                var entity = new Order
                {
                    CustomerId = model.CustomerId,
                    ProvinceCode = model.ProvinceCode,
                    State = true,
                    CreatedBy = model.CreatedBy,
                    CreatedAt = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var item in model.OrderItems)
                {
                    entity.OrderItems.Add(
                        new OrderItem
                        {
                            IdMenuItem = item.IdMenuItem,
                            Quantity = item.Quantity,
                        });
                }

                var checkOrderStock = _stockService.CheckOrderStock(entity.OrderItems);

                if (!checkOrderStock.IsSuccess)
                {
                    throw new Exception("No se pudo validar el stock.");
                }
                else if (checkOrderStock.Data != null && checkOrderStock.Data.ProductsOutOfStock != null
                    && checkOrderStock.Data.ProductsOutOfStock is List<Product> productsOutOfStock
                    && productsOutOfStock.Count > 0)
                {
                    foreach (var item in productsOutOfStock)
                    {
                        response.ValidationMessages.Add($"El producto '{item.Name}' (id: {item.Id}) no tiene suficiente stock");
                    }
                    throw new Exception("No hay suficiente stock para registrar el pedido.");
                }

                _unitOfWork.BeginTransaction();
                await _unitOfWork.OrderRepository.Add(entity);
                if(checkOrderStock.Data != null 
                    && checkOrderStock.Data.ProductsWithStockToBeReduced != null
                    && checkOrderStock.Data.ProductsWithStockToBeReduced is List<Product> productsWithStockToBeReduced
                    && productsWithStockToBeReduced.Count > 0)
                {
                    
                    var productsWithStockToUpdate = await _unitOfWork.ProductRepository
                    .GetBy(item => productsWithStockToBeReduced.Select(x => x.Id).Contains(item.Id))
                    .ToListAsync();

                    productsWithStockToUpdate = productsWithStockToUpdate
                    .Join(productsWithStockToBeReduced, product => product.Id, stockToBeReduced => stockToBeReduced.Id,
                    (product, stockToBeReduced) =>
                    {
                        product.Stock -= stockToBeReduced.Stock;
                        return product;
                    }
                    ).ToList();

                    _unitOfWork.ProductRepository.UpdateRange(productsWithStockToUpdate);
                }
                _unitOfWork.CommitTransaction();

                response.Data = entity;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo registrar el item. Exception: {ex.Message}");
                _unitOfWork.RollbackTransaction();
            }
            return response;
        }
    }
}
