using PinewoodDmsApi.Dtos;

namespace PinewoodDmsApi.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetCustomers();
        Task<CustomerDTO?> GetCustomer(int id);
        Task<int> InsertCustomer(InsertCustomerDTO newCustomer);
        Task<bool> IsCustomerExists(int id);
        Task<int> UpdateCustomer(UpdateCustomerDTO updatedCustomer);
        Task<int> DeleteCustomer(int id);
    }
}
