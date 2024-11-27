using _03._Dapper.Models;
using _03._Dapper.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _03._Dapper.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var users = await customerRepository.GetAllCustomersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await customerRepository.GetCustomerByIdAsync(id);
            return Ok(user);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel customer)
        {
            var user = await customerRepository.CreateCustomerAsync(customer);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id,[FromBody] CustomerModel customer)
        {
            var user = await customerRepository.UpdateCustomerAsync(id, customer);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedCustomer = await customerRepository.DeleteCustomerAsync(id);
            return Ok("Deleted Customer");
        }
    }
}
