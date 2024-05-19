using BeSpokedBikesAPI.Data;
using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Dapper;
using System.Data;

namespace BeSpokedBikesAPI.Repository
{
    public class SalespersonRepository : ISalespersonRepository
    {
        private readonly DapperContext _context;

        public SalespersonRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> SalesPersonExistsById(int id)
        {
            var query = "SELECT COUNT(*) FROM Salespersons WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = id });
                return count > 0;
            }
        }

        public async Task<IEnumerable<Salesperson>> GetAll()
        {
            var query = "SELECT * FROM Salespersons";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Salesperson>(query);
            }
        }

        public async Task<Salesperson> GetById(int id)
        {
            // Check if the customer exists before attempting to retrieve it
            bool exists = await SalesPersonExistsById(id);

            if (!exists)
            {
                // Handle the case where the customer does not exist
                throw new Exception("Salesperson not found");
            }
            else
            {
                // Retrieve the customer if it exists
                var query = "SELECT * FROM Salespersons WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QuerySingleOrDefaultAsync<Salesperson>(query, new { Id = id });
                }
            }
        }

        public async Task<bool> SalesPersonExists(Salesperson salesperson)
        {
            var query = "SELECT COUNT(*) FROM Salespersons WHERE FirstName = @FirstName AND LastName = @LastName";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, salesperson);
                return count > 0;
            }
        }

        public async Task Update(Salesperson salesperson)
        {
            // Check if a customer with the same FirstName, lastname already exists
            bool isDuplicate = await SalesPersonExists(salesperson);

            if (isDuplicate)
            {
                // Handle duplicate customer
                // For example, you can throw an exception or return a flag indicating duplication
                throw new Exception("Duplicate salesperson");
            }
            else
            {
                // Update the customer if no duplicate is found
                var sql = @"
                       UPDATE Salespersons
                       SET FirstName = @FirstName, LastName = @LastName, Address = @Address, 
                           Phone = @Phone, StartDate = @StartDate, TerminationDate = @TerminationDate, Manager = @Manager
                       WHERE Id = @Id";

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, salesperson);
                }
            }
        }


    }

}
