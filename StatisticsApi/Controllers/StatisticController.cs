using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace StatisticsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        [Route("statistic/get")]
        [HttpGet]
        public async Task<Dictionary<string, int>> Get()
        {
            var client = new RestClient("https://localhost:7145/api/Votes/votes/get");
            var request = new RestRequest("", Method.Get);
            var response = await client.GetAsync<Dictionary<string, int>>(request);
            return response;
        }
    }
}
