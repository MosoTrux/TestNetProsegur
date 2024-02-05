using TestNetProsegur.Core.Models.Mockapi.io;

namespace TestNetProsegur.Core.Repositories.Mockapi.io
{
    public interface IMockapiIORespository
    {
        List<GetTaxResponse> GetTaxes();
    }
}
