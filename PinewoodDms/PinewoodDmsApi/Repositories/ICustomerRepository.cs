using PinewoodDmsApi.Models;

namespace PinewoodDmsApi.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer?> GetCustomer(int id);
        Task<int> InsertCustomer(Customer customer);
        Task<bool> IsCustomerExists(int id);
        Task<int> UpdateCustomer(Customer updatedCustomer);
        Task<int> DeleteCustomer(int id);
    }
}
