using Npgsql;
using SponsorAPI.Models;


namespace SponsorAPI.DAO
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresDB");
        }

        public async Task<bool> PaymentExistsAsync(int contractId, DateTime paymentDate, decimal amountPaid)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT COUNT(1) 
                FROM sports.Payments 
                WHERE ContractID = @ContractID AND PaymentDate = @PaymentDate AND AmountPaid = @AmountPaid";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContractID", contractId);
                    command.Parameters.AddWithValue("@PaymentDate", paymentDate);
                    command.Parameters.AddWithValue("@AmountPaid", amountPaid);

                    var exists = (long)await command.ExecuteScalarAsync();
                    return exists > 0;
                }
            }
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                INSERT INTO sports.Payments (PaymentDate, AmountPaid, PaymentStatus)
                VALUES ( @PaymentDate, @AmountPaid, @PaymentStatus)
                RETURNING PaymentID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    command.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                    command.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }
    }
}
