using Votes.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace VotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        [Route("votes/get")]
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            return Singleton.Instance.votesDictionary;
        }

        [Route("votes/post")]
        [HttpPost]
        public IActionResult Post(string candidate, int dpi)
        {
            if (Singleton.Instance.state == "open" && Singleton.Instance.votesDictionary.ContainsKey(candidate) && !Singleton.Instance.dpiuser.Contains(dpi))
            {
                Singleton.Instance.votesDictionary[candidate] += 1;
                Singleton.Instance.dpiuser.Add(dpi);
                return Ok(Singleton.Instance.votesDictionary[candidate]);
            }
            if (Singleton.Instance.dpiuser.Contains(dpi))
            {
                Singleton.Instance.votesDictionary["Fraude"] += 1;
            }

            return BadRequest();
        }

        [Route("votes/open")]
        [HttpGet]
        public async Task<IActionResult> Open()
        {
            if (!Singleton.Instance.wasopen)
            {
                Singleton.Instance.state = "open";
                Singleton.Instance.wasopen = true;
                var client = new RestClient("https://localhost:7142/api/Candidate/candidate/get");
                var request = new RestRequest("", Method.Get);
                var response = await client.GetAsync<List<string>>(request);
                Singleton.Instance.votesDictionary.Add("nulo", 0);
                Singleton.Instance.votesDictionary.Add("Fraude", 0);
                foreach (var item in response)
                {
                    if (!Singleton.Instance.votesDictionary.ContainsKey(item))
                    {
                        Singleton.Instance.votesDictionary.Add(item, 0);
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

        [Route("votes/close")]
        [HttpGet]
        public IActionResult Close()
        {
            Singleton.Instance.state = "close";
            return Ok();
        }
    }
}
