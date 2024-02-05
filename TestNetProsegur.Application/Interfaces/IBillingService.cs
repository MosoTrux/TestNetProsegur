using TestNetProsegur.Application.Dtos;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IBillingService
    {
        Task<ServiceResponseDto<GetInvoiceDto>> GetInvoice(long orderId);
    }
}
