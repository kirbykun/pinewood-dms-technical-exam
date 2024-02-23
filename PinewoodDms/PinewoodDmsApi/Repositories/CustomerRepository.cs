using Microsoft.EntityFrameworkCore;
using PinewoodDmsApi.Data;
using PinewoodDmsApi.Models;

namespace PinewoodDmsApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _dataContext;

        public CustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Customer>> GetCustomers() => await _dataContext.Customers.ToListAsync();

        public async Task<Customer?> GetCustomer(int id) => await _dataContext.Customers.FindAsync(id);

        public async Task<int> InsertCustomer(Customer customer)
        {
            await _dataContext.AddAsync(customer);
            return await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> IsCustomerExists(int id) => await _dataContext.Customers.AnyAsync(x => x.Id == id);

        public async Task<int> UpdateCustomer(Customer updatedCustomer)
        {
            return await _dataContext.Customers
                            .Where(x => x.Id == updatedCustomer.Id)
                            .ExecuteUpdateAsync(s =>
                                s.SetProperty(x => x.FirstName, updatedCustomer.FirstName)
                                 .SetProperty(x => x.LastName, updatedCustomer.LastName)
                                 .SetProperty(x => x.Email, updatedCustomer.Email)
                                 .SetProperty(x => x.Phone, updatedCustomer.Phone)
                                 .SetProperty(x => x.UpdatedDttm, updatedCustomer.UpdatedDttm)
                            );
        }

        public async Task<int> DeleteCustomer(int id) => await _dataContext.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
