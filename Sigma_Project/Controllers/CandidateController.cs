using Microsoft.AspNetCore.Mvc;
using Sigma_Project.Models;
using Sigma_Project.Services;

namespace Sigma_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        public IActionResult GetCandidates()
        {
            var candidates = _candidateRepository.GetAllCandidates();
            return Ok(candidates);
        }

        [HttpGet("{email}")]
        public IActionResult GetCandidate(string email)
        {
            var candidate = _candidateRepository.GetCandidateByEmail(email);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        [HttpPost]
        public IActionResult AddOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _candidateRepository.AddOrUpdateCandidate(candidate);
            return Ok();
        }
    }
}
