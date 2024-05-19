using BeSpokedBikesAPI.Data;
using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Dapper;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BeSpokedBikesAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DapperContext _context;

        public SaleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SaleDetailsDto>> GetSalesAsync(DateTime? startDate, DateTime? endDate)
        {
            var sql = @"
                        SELECT 
                           s.Id, 
                           p.Name AS Product, 
                           CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName, 
                           s.SalesDate AS SalesDate, 
                           p.SalePrice AS Price, 
                           CONCAT(sp.FirstName, ' ', sp.LastName) AS SalespersonName, 
                           (p.CommissionPercentage * p.SalePrice) AS SalespersonCommission
                       FROM 
                           Sales s
                       JOIN 
                           Products p ON s.ProductId = p.Id
                       JOIN 
                           Customers c ON s.CustomerId = c.Id
                       JOIN 
                           Salespersons sp ON s.SalespersonId = sp.Id
                       WHERE 
                           (@startDate IS NULL OR s.SalesDate >= @startDate)
                           AND (@endDate IS NULL OR s.SalesDate <= @endDate)
                       ORDER BY s.SalesDate DESC";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<SaleDetailsDto>(sql, new { StartDate = startDate, EndDate = endDate });
            }
        }

        public async Task CreateSaleAsync(Sale sale)
        {
            var sql = @"
            INSERT INTO Sales (ProductId, CustomerId, SalespersonId, SalesDate, SalePrice, Commission)
            VALUES (@ProductId, @CustomerId, @SalespersonId, @SalesDate, @SalePrice, @Commission)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, sale);
            }
        }

    }

}
