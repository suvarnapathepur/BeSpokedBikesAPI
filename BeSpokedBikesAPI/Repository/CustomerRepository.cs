using BeSpokedBikesAPI.Data;
using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Dapper;

namespace BeSpokedBikesAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;

        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> CustomerExistsById(int id)
        {
            var query = "SELECT COUNT(*) FROM Customers WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = id });
                return count > 0;
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var query = "SELECT * FROM Customers";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Customer>(query);
            }
        }

        public async Task<Customer> GetById(int id)
        {
            // Check if the customer exists before attempting to retrieve it
            bool exists = await CustomerExistsById(id);

            if (!exists)
            {
                // Handle the case where the customer does not exist
                throw new Exception("ustomer not found");
            }
            else
            {
                // Retrieve the customer if it exists
                var query = "SELECT * FROM Customers WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QuerySingleOrDefaultAsync<Customer>(query, new { Id = id });
                }
            }
        }


        public async Task<bool> CustomerExists(Customer customer)
        {
            var query = "SELECT COUNT(*) FROM Customers WHERE FirstName = @FirstName AND LastName = @LastName";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, customer);
                return count > 0;
            }
        }

        public async Task Add(Customer customer)
        {
            if (await CustomerExists(customer))
            {
                // Handle duplicate customer
                // You may throw an exception, return a flag, or handle it as appropriate
                throw new Exception("Duplicate customer");
            }
            else
            {
                var query = "INSERT INTO Customers(FirstName, LastName, Address, Phone, StartDate) VALUES(@FirstName, @LastName, @Address, @Phone, @StartDate)";
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, customer);
                }
            }
        }
        public async Task Update(Customer customer)
        {
            // Check if a customer with the same FirstName, lastname already exists
            bool isDuplicate = await CustomerExists(customer);

            if (isDuplicate)
            {
                // Handle duplicate customer
                // For example, you can throw an exception or return a flag indicating duplication
                throw new Exception("Duplicate customer");
            }
            else
            {
                // Update the customer if no duplicate is found
                var query = @"UPDATE Customers 
                      SET FirstName = @FirstName, LastName = @LastName, Address = @Address, 
                          Phone = @Phone, StartDate = @StartDate
                      WHERE Id = @Id";

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, customer);
                }
            }
        }

        public async Task Delete(int id)
        {
            // Check if the Customer exists before attempting to delete it
            bool exists = await CustomerExistsById(id);

            if (!exists)
            {
                // Handle the case where the Customer does not exist
                throw new Exception("Customer not found");
            }
            else
            {
                // Proceed with the deletion if the Customer exists
                var query = "DELETE FROM Customers WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Id = id });
                }
            }
        }


    }

}