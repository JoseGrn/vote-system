using Candidates.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        [Route("candidate/get")]
        [HttpGet]
        public List<string> Get()
        {
            return Singleton.Instance.candidatesList;
        }

        [Route("candidate/post")]
        [HttpPost]
        public IActionResult Post(string candidate)
        {
            if (Singleton.Instance.state == "open")
            {
                Singleton.Instance.candidatesList.Add(candidate);
                return Ok(candidate);
            }

            return BadRequest();
        }

        [Route("candidate/open")]
        [HttpGet]
        public IActionResult Open()
        {
            if (!Singleton.Instance.wasopen)
            {
                Singleton.Instance.state = "open";
                Singleton.Instance.wasopen = true;
                return Ok();
            }
            return BadRequest();
        }

        [Route("candidate/close")]
        [HttpGet]
        public IActionResult Close()
        {
            Singleton.Instance.state = "close";
            return Ok();
        }
    }
}
