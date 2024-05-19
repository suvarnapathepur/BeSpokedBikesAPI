using BeSpokedBikesAPI.Interface;
using BeSpokedBikesAPI.Model;
using BeSpokedBikesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BeSpokedBikesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _customerRepository.Add(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            var existingCustomer = await _customerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            customer.Id = id;
            await _customerRepository.Update(customer);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerRepository.Delete(id);
                return NoContent();
            }
            catch (SqlException ex) when (ex.Number == 547) // Foreign Key violation
            {
                return BadRequest(new { message = "Cannot delete customer as it is referenced in sales." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the customer." });
            }
        }
    }
}

