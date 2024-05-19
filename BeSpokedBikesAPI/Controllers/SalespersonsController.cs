using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeSpokedBikesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalespersonController : ControllerBase
    {
        private readonly ISalespersonRepository _salespersonRepository;

        public SalespersonController(ISalespersonRepository salespersonRepository)
        {
            _salespersonRepository = salespersonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalespersons()
        {
            var salespersons = await _salespersonRepository.GetAll();
            return Ok(salespersons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalespersonById(int id)
        {
            var salesperson = await _salespersonRepository.GetById(id);
            if (salesperson == null)
            {
                return NotFound();
            }
            return Ok(salesperson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesperson(int id, [FromBody] Salesperson salesperson)
        {
            if (id != salesperson.Id)
            {
                return BadRequest();
            }

            await _salespersonRepository.Update(salesperson);
            return NoContent();
        }
    }

}
