using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SponsorAPI.DAO;

namespace SponsorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorMatchCountController : ControllerBase
    {
        private readonly ISponsorMatchCount _sponsorMatchCount;

        public SponsorMatchCountController(ISponsorMatchCount sponsorMatchCount)
        {
            _sponsorMatchCount = sponsorMatchCount;
        }

        [HttpGet("match-count")]
        public async Task<IActionResult> GetSponsorMatchCount([FromQuery] int year)
        {
            if (year <= 0)
            {
                return BadRequest("Invalid year provided.");
            }

            var matchCounts = await _sponsorMatchCount.GetSponsorMatchCountByYearAsync(year);
            return Ok(matchCounts);
        }
    }
}
