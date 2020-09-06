using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Mvc.Models.Dto;
using Newtonsoft.Json;

namespace MicroRabbit.Mvc.Services
{
    public class TransferService : ITransferService
    {
        private readonly HttpClient _apiClient;


        public TransferService(HttpClient apiClient) {
            _apiClient = apiClient;
        }

        public async Task Transfer(TransferDto transferDto) {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(transferDto), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _apiClient.PostAsync("api/banking/accounts/transfer-funds", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
