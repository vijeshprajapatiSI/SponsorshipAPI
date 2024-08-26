using SponsorAPI.DAO;
using SponsorAPI.Models;
namespace SponsorAPI.DAO
{
    public interface IPaymentRepository
    {
        Task<bool> PaymentExistsAsync(int contractId, DateTime paymentDate, decimal amountPaid);
        Task<int> AddPaymentAsync(Payment payment);
    }
}
