using Microsoft.AspNetCore.Mvc;
using PinewoodDmsApi.Dtos;
using PinewoodDmsApi.Services;

namespace PinewoodDmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetCustomers();
                if (customers.Count == 0) { return NotFound("Customers not available"); }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomer(id);
                if (customer is null) return NotFound("Customer doesn't exist");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomer(InsertCustomerDTO newCustomer)
        {
            try
            {
                var result = await _customerService.InsertCustomer(newCustomer);
                if (result == 0) return BadRequest("Failed to add customer");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO updatedCustomer)
        {
            try
            {
                var isCustomerExists = await _customerService.IsCustomerExists(updatedCustomer.Id);
                if (!isCustomerExists) return NotFound("Customer doesn't exist");

                var result = await _customerService.UpdateCustomer(updatedCustomer);
                if (result == 0) return BadRequest("Failed to update customer");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(id);
                if (result == 0) return BadRequest("Failed to remove customer");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
