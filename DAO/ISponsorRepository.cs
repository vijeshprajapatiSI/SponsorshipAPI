using SponsorAPI.Models;
namespace SponsorAPI.DAO
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<SponsorPaymentSummary>> GetSponsorPaymentSummariesAsync();

    }
}
