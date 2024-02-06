using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Dtos.Order;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IBillingService
    {
        Task<ServiceResponseDto<GetInvoiceDto>> GetInvoice(long orderId);
    }
}
