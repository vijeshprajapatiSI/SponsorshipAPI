using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SponsorAPI.DAO;

namespace SponsorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorRepository _sponsorRepository;

        public SponsorController(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        [HttpGet("payment-summary")]
        public async Task<IActionResult> GetSponsorPaymentSummaries()
        {
            var summaries = await _sponsorRepository.GetSponsorPaymentSummariesAsync();
            return Ok(summaries);
        }
    }
}
