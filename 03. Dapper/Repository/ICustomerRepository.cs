using _03._Dapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _03._Dapper.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerModel>> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerModel>> CreateCustomerAsync(CustomerModel customer);
        Task<CustomerModel> UpdateCustomerAsync(int id, CustomerModel customer);
        Task<string> DeleteCustomerAsync(int customerId);
    }
}