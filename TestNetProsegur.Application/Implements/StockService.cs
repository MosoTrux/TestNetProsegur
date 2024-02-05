using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.ProductDto;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;

namespace TestNetProsegur.Application.Implements
{
    public class StockService : IStockService
    {
        private readonly IRepository<Product> _productoRepository;
        private readonly IRepository<Ingredient> _ingredientRepository;

        public StockService(IRepository<Product> productoRepository,
            IRepository<Ingredient> ingredientRepository)
        {
            _productoRepository = productoRepository;
            _ingredientRepository = ingredientRepository;
        }

        public ServiceResponseDto<CheckOrderStockResponseDto> CheckOrderStock(List<OrderItem> OrderItems)
        {
            var response = new ServiceResponseDto<CheckOrderStockResponseDto>();
            try
            {
                if (!OrderItems.Any())
                {
                    throw new Exception("El detalle de la orden no contiene items.");
                }

                var menuItemIds = OrderItems.Select(item => item.IdMenuItem);
                var ingredients = _ingredientRepository.GetBy(item => menuItemIds.Contains(item.IdMenuItem));

                var groupedIngredients = ingredients.ToList()
                .Join(OrderItems, ingredient => ingredient.IdMenuItem, orderItem => orderItem.IdMenuItem,
                    (ingredient, orderItem) => new { ingredient, orderItem })
                .GroupBy(grouped => grouped.ingredient.IdProduct)
                .Select(grouped => new
                {
                    IdProduct = grouped.Key,
                    TotalQuantity = grouped.Sum(x => x.ingredient.Quantity * x.orderItem.Quantity)
                });

                var productIds = groupedIngredients.Select(i => i.IdProduct);
                var products = _productoRepository.GetBy(prod => productIds.Contains(prod.Id)).ToList();

                var productsOutOfStock = products
                    .Join(groupedIngredients, product => product.Id, ingredient => ingredient.IdProduct,
                            (product, ingredient) => new { Product = product, Ingredient = ingredient })
                    .Where(combined => combined.Product.Stock < combined.Ingredient.TotalQuantity)
                    .Select(combined => combined.Product).ToList();

                var productsInStock = products.Except(productsOutOfStock).ToList();

                var productsWithStockToBeReduced = productsInStock
                    .Join(groupedIngredients, product => product.Id, ingredient => ingredient.IdProduct,
                        (product, ingredient) => new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Unit = product.Unit,
                            Stock = ingredient.TotalQuantity
                        })
                    .ToList();

                var result = new CheckOrderStockResponseDto
                {
                    ProductsInStock = productsInStock,
                    ProductsWithStockToBeReduced = productsWithStockToBeReduced,
                    ProductsOutOfStock = productsOutOfStock
                };

                response.Data = result;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo validar el stock. Exception: {ex.Message}");
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponseDto<List<Product>>> AddStock(List<AddStockDto> model)
        {
            var response = new ServiceResponseDto<List<Product>>();
            try
            {
                var entities = await _productoRepository
                    .GetBy(item => model.Select(x => x.ProductId).Contains(item.Id))
                    .ToListAsync();

                if(!entities.Any())
                {
                    throw new Exception("Los productos no existen.");
                }

                entities = entities
                    .Join(model, entity => entity.Id, addStock => addStock.ProductId,
                    (entity, addStock) =>
                    {
                        entity.Stock += addStock.Stock;
                        return entity;
                    }
                    ).ToList();

                _productoRepository.UpdateRange(entities);
                await _productoRepository.SaveChangesAsync();
                response.Data = entities;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo actualizar el stock. Exception: {ex.Message}");
            }
            return response;
        }

    }
}
