using BeSpokedBikesAPI.Data;
using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Dapper;

namespace BeSpokedBikesAPI.Repository
{
    public class CommissionReportRepository : ICommissionReportRepository
    {
        private readonly DapperContext _context;

        public CommissionReportRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<QuarterlyCommissionReport>> GetQuarterlyCommissionReportAsync(int year)
        {
            var sql = @"
            SELECT 
                sp.Id AS SalespersonId,
                CONCAT(sp.FirstName, ' ', sp.LastName) AS Salesperson, 
                DATEPART(YEAR, s.SalesDate) AS Year,
                DATEPART(QUARTER, s.SalesDate) AS Quarter,
                SUM(p.SalePrice * (p.CommissionPercentage / 100)) AS TotalCommission
            FROM Sales s
            JOIN Products p ON s.ProductId = p.Id
            JOIN Salespersons sp ON s.SalespersonId = sp.Id
            WHERE DATEPART(YEAR, s.SalesDate) = @Year
            GROUP BY sp.Id, sp.FirstName, sp.LastName, DATEPART(YEAR, s.SalesDate), DATEPART(QUARTER, s.SalesDate)
            ORDER BY Year, Quarter, sp.LastName, sp.FirstName"
            ;
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<QuarterlyCommissionReport>(sql, new { Year = year });
            }
        }
    }

}
