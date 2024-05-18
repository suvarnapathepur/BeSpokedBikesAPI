using BeSpokedBikesAPI.Data;
using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Dapper;

namespace BeSpokedBikesAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> ProductExistsById(int id)
        {
            var query = "SELECT COUNT(*) FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = id });
                return count > 0;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Product>(query);
            }
        }

        public async Task<Product> GetById(int id)
        {
            // Check if the product exists before attempting to retrieve it
            bool exists = await ProductExistsById(id);

            if (!exists)
            {
                // Handle the case where the product does not exist
                throw new Exception("Product not found");
            }
            else
            {
                // Retrieve the product if it exists
                var query = "SELECT * FROM Products WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
                }
            }
        }


        public async Task<bool> ProductExists(Product product)
        {
            var query = "SELECT COUNT(*) FROM Products WHERE Name = @Name AND Manufacturer = @Manufacturer AND Style = @Style";
            using (var connection = _context.CreateConnection())
            {
                var count = await connection.QuerySingleOrDefaultAsync<int>(query, product);
                return count > 0;
            }
        }

        public async Task Add(Product product)
        {
            if (await ProductExists(product))
            {
                // Handle duplicate product
                // You may throw an exception, return a flag, or handle it as appropriate
                throw new Exception("Duplicate product");
            }
            else
            {
                var query = "INSERT INTO Products (Name, Manufacturer, Style, PurchasePrice, SalePrice, QtyOnHand, CommissionPercentage) VALUES (@Name, @Manufacturer, @Style, @PurchasePrice, @SalePrice, @QtyOnHand, @CommissionPercentage)";
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, product);
                }
            }
        }

        public async Task Update(Product product)
        {
            // Check if a product with the same Name, Manufacturer, and Style already exists
            bool isDuplicate = await ProductExists(product);

            if (isDuplicate)
            {
                // Handle duplicate product
                // For example, you can throw an exception or return a flag indicating duplication
                throw new Exception("Duplicate product");
            }
            else
            {
                // Update the product if no duplicate is found
                var query = @"UPDATE Products 
                      SET Name = @Name, Manufacturer = @Manufacturer, Style = @Style, 
                          PurchasePrice = @PurchasePrice, SalePrice = @SalePrice, 
                          QtyOnHand = @QtyOnHand, CommissionPercentage = @CommissionPercentage 
                      WHERE Id = @Id";

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, product);
                }
            }
        }

        public async Task Delete(int id)
        {
            // Check if the product exists before attempting to delete it
            bool exists = await ProductExistsById(id);

            if (!exists)
            {
                // Handle the case where the product does not exist
                throw new Exception("Product not found");
            }
            else
            {
                // Proceed with the deletion if the product exists
                var query = "DELETE FROM Products WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Id = id });
                }
            }
        }


    }

}
