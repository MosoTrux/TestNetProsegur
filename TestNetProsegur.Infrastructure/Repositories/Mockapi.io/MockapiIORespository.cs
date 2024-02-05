using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TestNetProsegur.Core.Models.Mockapi.io;
using TestNetProsegur.Core.Repositories.Mockapi.io;

namespace TestNetProsegur.Infrastructure.Repositories.Mockapi.io
{
    public class MockapiIORespository : IMockapiIORespository
    {
        private IConfiguration _configuration;
        public MockapiIORespository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<GetTaxResponse> GetTaxes()
        {
            var result = new List<GetTaxResponse>();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(GetURL()).Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        result = JsonSerializer.Deserialize<List<GetTaxResponse>>(json);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return result;
        }

        private string GetURL()
        {
            IConfigurationSection mockapiIO = _configuration.GetSection("AppSettings:MockapiIO");
            IEnumerable<IConfigurationSection> apis = mockapiIO.GetChildren();

            var url = apis
                .Where(config => config["Name"]!.ToString().Equals("GetTaxes"))
                .Select(config => config["Url"]!.ToString())
                .FirstOrDefault();

            return url;
        }
    }
}
