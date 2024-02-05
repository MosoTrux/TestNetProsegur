using Microsoft.Extensions.Caching.Memory;
using System.Text.Encodings.Web;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using TestNetProsegur.Core.Repositories.Mockapi.io;

namespace TestNetProsegur.Application.Implements
{
    public class MockapiIOService : IMockapiIOService
    {
        private readonly IMockapiIORespository _mockapiIORepository;
        private readonly IMemoryCache _cache;

        public MockapiIOService(IMockapiIORespository mockapiIORepository, IMemoryCache cache)
        {
            _mockapiIORepository = mockapiIORepository;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public decimal GetTax(string provinceCode)
        {
            var taxes = GetTaxes();
            taxes.TryGetValue(provinceCode, out decimal tax);
            return tax;
        }

        private Dictionary<string, decimal> GetTaxes()
        {
            var cacheKey = "Taxes";

            if (_cache.TryGetValue(cacheKey, out Dictionary<string, decimal> cachedTaxes))
            {
                return cachedTaxes;
            }
            else
            {
                var taxesResponse = _mockapiIORepository.GetTaxes();
                var taxes = new Dictionary<string, decimal>();
                if(taxesResponse == null || taxesResponse.Count == 0) 
                {
                    taxes = LoadTaxes();
                } 
                else
                {
                    taxes = taxesResponse.ToDictionary(item => item.id, item => item.tax);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                _cache.Set(cacheKey, taxes, cacheEntryOptions);

                return taxes;
            }
        }

        private Dictionary<string, decimal> LoadTaxes()
        {
            return new Dictionary<string, decimal>
            {
                { "1", 0.15M },
                { "2", 0.16M },
                { "3", 0.17M },
                { "4", 0.18M },
                { "5", 0.19M },
                { "6", 0.16M },
                { "7", 0.17M },
                { "8", 0.18M },
                { "9", 0.19M },
                { "10", 0.15M },
                { "11", 0.16M },
                { "12", 0.17M },
                { "13", 0.18M },
                { "14", 0.19M },
                { "15", 0.15M },
                { "16", 0.16M },
                { "17", 0.17M },
                { "18", 0.18M },
                { "19", 0.19M },
                { "20", 0.15M },
                { "21", 0.16M },
                { "22", 0.17M },
                { "23", 0.18M },
                { "24", 0.19M },
                { "25", 0.15M }
            };
        }
    }
}
