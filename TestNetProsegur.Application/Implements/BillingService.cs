using Microsoft.EntityFrameworkCore;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Order;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;

namespace TestNetProsegur.Application.Implements
{
    public class BillingService : IBillingService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMockapiIOService _mockapiIOService;

        public BillingService(IRepository<Order> orderRepository, IMockapiIOService mockapiIOService)
        {
            _orderRepository = orderRepository;
            _mockapiIOService = mockapiIOService;
        }

        public async Task<ServiceResponseDto<GetInvoiceDto>> GetInvoice(long orderId)
        {
            var response = new ServiceResponseDto<GetInvoiceDto>();
            try
            {
                var order = await _orderRepository
                    .GetBy(order => order.Id == orderId)
                    .Include(d => d.OrderItems)
                        .ThenInclude(d => d.IdMenuItemNavigation)
                    .FirstOrDefaultAsync();

                if(order == null)
                {
                    throw new Exception("La orden no existe.");
                }

                var tax = _mockapiIOService.GetTax(order.ProvinceCode);

                var invoiceItems = order.OrderItems
                    .Select(item => new ItemInvoiceDto
                    {
                        NameMenuItem = item.IdMenuItemNavigation.Name,
                        Price = item.IdMenuItemNavigation.Price,
                        Quantity = item.Quantity,
                        SubTotalItem = item.Quantity * item.IdMenuItemNavigation.Price
                    }).ToList();

                var subTotal = invoiceItems.Sum(x => x.SubTotalItem);
                var totalTax = subTotal * tax;

                var invoice = new GetInvoiceDto
                {
                    Customer = "Juan Perez",
                    Employeed = "John Doe",
                    OrderId = orderId,
                    Province = GlobalVar.Provinces[order.ProvinceCode],
                    InvoiceItems = invoiceItems,
                    SubTotal = subTotal,
                    Tax = totalTax,
                    Total = subTotal + totalTax
                };

                response.Data = invoice;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ValidationMessages.Add($"No se pudo obtener el item. Exception: {ex.Message}");
                response.IsSuccess = false;
            }
            return response;
        }

    }
}
