using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var sales = await _saleRepository.GetSalesAsync(startDate, endDate);
            return Ok(sales);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            await _saleRepository.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetSales), new { id = sale.Id }, sale);
        }
    }

}
