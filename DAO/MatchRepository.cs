using Npgsql;
using System.Text.RegularExpressions;
using SponsorAPI.Models;

namespace SponsorAPI.DAO
{
    public class MatchRepository : IMatchRepository
    {
        private readonly string _connectionString;

        public MatchRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresDB");
        }




        public async Task<IEnumerable<Matches>> GetMatchesWithTotalPaymentsAsync()
        {
            var matches = new List<Matches>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"SELECT M.MatchID, M.MatchName, M.MatchDate, M.Location, COALESCE(SUM(P.AmountPaid), 0) AS TotalAmountPaid
                          FROM sports.Matches M
                          LEFT JOIN sports.Contracts C ON M.MatchID = C.MatchID
                          LEFT JOIN sports.Payments P ON C.ContractID = P.ContractID
                          GROUP BY M.MatchID, M.MatchName, M.MatchDate, M.Location";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            matches.Add(new Matches
                            {
                                MatchID = reader.GetInt32(0),
                                MatchName = reader.GetString(1),
                                MatchDate = reader.GetDateTime(2),
                                Location = reader.GetString(3),
                                TotalAmountPaid = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }

            return matches;
        }
    }
}
