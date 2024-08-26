using SponsorAPI.Models;
namespace SponsorAPI.DAO
{
    public interface ISponsorMatchCount
    {
        Task<IEnumerable<SponsorMatchCount>> GetSponsorMatchCountByYearAsync(int year);

    }
}
