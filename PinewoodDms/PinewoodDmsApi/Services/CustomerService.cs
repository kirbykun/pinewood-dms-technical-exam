using AutoMapper;
using PinewoodDmsApi.Dtos;
using PinewoodDmsApi.Models;
using PinewoodDmsApi.Repositories;

namespace PinewoodDmsApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper) {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }    

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomers();

            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO?> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<int> InsertCustomer(InsertCustomerDTO newCustomer)
        {
            var customer = _mapper.Map<Customer>(newCustomer);
            customer.CreatedDttm = DateTime.Now;
            customer.UpdatedDttm = DateTime.Now;

            return await _customerRepository.InsertCustomer(customer);
        }

        public async Task<bool> IsCustomerExists(int id)
        {
            return await _customerRepository.IsCustomerExists(id);
        }

        public async Task<int> UpdateCustomer(UpdateCustomerDTO updatedCustomer)
        {
            var customer = _mapper.Map<Customer>(updatedCustomer);
            customer.UpdatedDttm = DateTime.Now;

            return await _customerRepository.UpdateCustomer(customer);
        }

        public async Task<int> DeleteCustomer(int id)
        {
            return await _customerRepository.DeleteCustomer(id);
        }
    }
}
