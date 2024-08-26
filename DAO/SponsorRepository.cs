using Npgsql;
using SponsorAPI.Models;


namespace SponsorAPI.DAO
{
    public class SponsorRepository : ISponsorRepository
    {
        
        
            private readonly string _connectionString;

            public SponsorRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("PostgresDB");
            }

            public async Task<IEnumerable<SponsorPaymentSummary>> GetSponsorPaymentSummariesAsync()
            {
                var summaries = new List<SponsorPaymentSummary>();

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"
                SELECT 
                    S.SponsorID,
                    S.SponsorName,
                    S.IndustryType,
                    S.ContactEmail,
                    COALESCE(SUM(P.AmountPaid), 0) AS TotalPayments,
                    COUNT(P.PaymentID) AS NumberOfPayments,
                    MAX(P.PaymentDate) AS LatestPaymentDate
                FROM sports.Sponsors S
                LEFT JOIN sports.Contracts C ON S.SponsorID = C.SponsorID
                LEFT JOIN sports.Payments P ON C.ContractID = P.ContractID
                GROUP BY S.SponsorID, S.SponsorName, S.IndustryType, S.ContactEmail";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                summaries.Add(new SponsorPaymentSummary
                                {
                                    SponsorID = reader.GetInt32(0),
                                    SponsorName = reader.GetString(1),
                                    IndustryType = reader.GetString(2),
                                    ContactEmail = reader.GetString(3),
                                    TotalPayments = reader.GetDecimal(4),
                                    NumberOfPayments = reader.GetInt32(5),
                                    LatestPaymentDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }

                return summaries;
            }
        }
    }

