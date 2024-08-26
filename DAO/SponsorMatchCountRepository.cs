using Npgsql;
using SponsorAPI.Models;


namespace SponsorAPI.DAO
{
    public class SponsorMatchCountRepository : ISponsorMatchCount
    {
        private readonly string _connectionString;

        public SponsorMatchCountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresDB");
        }

        public async Task<IEnumerable<SponsorMatchCount>> GetSponsorMatchCountByYearAsync(int year)
        {
            var matchCounts = new List<SponsorMatchCount>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    S.SponsorID,
                    S.SponsorName,
                    COUNT(M.MatchID) AS MatchCount
                FROM sports.Sponsors S
                JOIN sports.Contracts C ON S.SponsorID = C.SponsorID
                JOIN sports.Matches M ON C.MatchID = M.MatchID
                WHERE EXTRACT(YEAR FROM M.MatchDate) = @Year
                GROUP BY S.SponsorID, S.SponsorName";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            matchCounts.Add(new SponsorMatchCount
                            {
                                SponsorID = reader.GetInt32(0),
                                SponsorName = reader.GetString(1),
                                MatchCount = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }

            return matchCounts;
        }
    }
}
