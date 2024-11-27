using _03._Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace _03._Dapper.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            //var customers = await dbConnection.QueryAsync<CustomerModel>("SELECT * FROM Customers");
            var customers = await dbConnection.QueryAsync<CustomerModel>("sp_GetAllCustomers",new { },commandType:CommandType.StoredProcedure);

            return customers;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerByIdAsync(int id)
        {
            var customer = await dbConnection.QueryAsync<CustomerModel>("sp_GetCustomerById", new {id }, commandType: CommandType.StoredProcedure);

            return customer;
        }

        public async Task<IEnumerable<CustomerModel>> CreateCustomerAsync(CustomerModel customer)
        {
            var parameters = new
            {
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.DateOfBirth
            };

            try
            {
                var newCustomer = await dbConnection.QueryAsync<CustomerModel>(
                    "sp_AddNewCustomer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                    return newCustomer;
            } catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating customer ! \n", ex);
            }
        }

        public async Task<CustomerModel> UpdateCustomerAsync(int id, CustomerModel customer)
        {
            var parameters = new
            {
                id,
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.DateOfBirth
            };

            try
            {
                var updatedCustomer = await dbConnection.QuerySingleOrDefaultAsync<CustomerModel>(
                    "sp_UpdateCustomer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (updatedCustomer == null)
                {
                    throw new Exception("Customer not found.");
                }

                return updatedCustomer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the customer.", ex);
            }
        }

        public async Task<string> DeleteCustomerAsync(int customerId)
        {
            var parameters = new { Id = customerId };

            try
            {
                var result = await dbConnection.QuerySingleOrDefaultAsync<dynamic>(
                    "sp_DeleteCustomer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (result == null)
                {
                    throw new Exception("Customer not found.");
                }

                return result.Message;
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
        }


    }
}
