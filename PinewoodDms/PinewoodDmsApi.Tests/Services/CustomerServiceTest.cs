using AutoMapper;
using Microsoft.OpenApi.Any;
using Moq;
using PinewoodDmsApi.Dtos;
using PinewoodDmsApi.Models;
using PinewoodDmsApi.Repositories;
using PinewoodDmsApi.Services;

namespace PinewoodDmsApi.Tests.Services
{
    public class CustomerServiceTest
    {
        [Fact]
        public async Task GetCustomers_Should_Return_List_Of_Mapped_Customers_Dto()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            var dttm = DateTime.Now;
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Jhon",
                    LastName = "Ambrad",
                    Email = "jhon.ambrad@gmail.com",
                    Phone = "63956011567",
                    CreatedDttm = dttm,
                    UpdatedDttm = dttm
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Kirby",
                    LastName = "Taghoy",
                    Email = "kirby.taghoy@gmail.com",
                    Phone = "639560320053",
                    CreatedDttm = dttm,
                    UpdatedDttm = dttm
                },
            };
            var mockMappedCustomers = new List<CustomerDTO>
            {
                new CustomerDTO
                {
                    Id = 1,
                    FirstName = "Jhon",
                    LastName = "Ambrad",
                    Email = "jhon.ambrad@gmail.com",
                    Phone = "63956011567",
                    CreatedDttm = dttm,
                    UpdatedDttm = dttm
                },
                new CustomerDTO
                {
                    Id = 2,
                    FirstName = "Kirby",
                    LastName = "Taghoy",
                    Email = "kirby.taghoy@gmail.com",
                    Phone = "639560320053",
                    CreatedDttm = dttm,
                    UpdatedDttm = dttm
                },
            };

            mockCustomerRepo.Setup(repo => repo.GetCustomers()).Returns(Task.FromResult(customers));
            mockMapper.Setup(mapper => mapper.Map<List<CustomerDTO>>(customers)).Returns(mockMappedCustomers);

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.GetCustomers();


            Assert.Equal(mockMappedCustomers.Count, result.Count);
            for (int i = 0; i < mockMappedCustomers.Count; i++)
            {
                Assert.Equal(mockMappedCustomers[i].Id, result[i].Id);
                Assert.Equal(mockMappedCustomers[i].FirstName, result[i].FirstName);
                Assert.Equal(mockMappedCustomers[i].LastName, result[i].LastName);
                Assert.Equal(mockMappedCustomers[i].Email, result[i].Email);
                Assert.Equal(mockMappedCustomers[i].Phone, result[i].Phone);
                Assert.Equal(mockMappedCustomers[i].CreatedDttm, result[i].CreatedDttm);
                Assert.Equal(mockMappedCustomers[i].UpdatedDttm, result[i].UpdatedDttm);
            }
        }

        [Fact]
        public async Task GetCustomer_Should_Return_Mapped_Customer_Dto()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            var dttm = DateTime.Now;
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
                CreatedDttm = dttm,
                UpdatedDttm = dttm
            };

            var mockMappedCustomer = new CustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
                CreatedDttm = dttm,
                UpdatedDttm = dttm
            };

            mockCustomerRepo.Setup(repo => repo.GetCustomer(It.IsAny<int>())).Returns(Task.FromResult(customer));
            mockMapper.Setup(mapper => mapper.Map<CustomerDTO>(customer)).Returns(mockMappedCustomer);

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.GetCustomer(It.IsAny<int>());


            Assert.Equal(mockMappedCustomer.Id, result.Id);
            Assert.Equal(mockMappedCustomer.FirstName, result.FirstName);
            Assert.Equal(mockMappedCustomer.LastName, result.LastName);
            Assert.Equal(mockMappedCustomer.Email, result.Email);
            Assert.Equal(mockMappedCustomer.Phone, result.Phone);
            Assert.Equal(mockMappedCustomer.CreatedDttm, result.CreatedDttm);
            Assert.Equal(mockMappedCustomer.UpdatedDttm, result.UpdatedDttm);
        }

        [Fact]
        public async Task InsertCustomer_Can_Insert()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            var dttm = DateTime.Now;
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
                CreatedDttm = dttm,
                UpdatedDttm = dttm
            };

            var insertCustomerDto = new InsertCustomerDTO
            {
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };

            //assuming customer was inserted successfully, return 1 from SaveChangesAsync() 
            mockCustomerRepo.Setup(repo => repo.InsertCustomer(customer)).Returns(Task.FromResult(1)); 
            mockMapper.Setup(mapper => mapper.Map<Customer>(insertCustomerDto)).Returns(customer);

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.InsertCustomer(insertCustomerDto);


            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, false)]
        public async Task IsCustomerExists_Should_Return_Correct_Value(int customerId, bool isCustomerExists)
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "Jhon",
                    LastName = "Ambrad",
                    Email = "jhon.ambrad@gmail.com",
                    Phone = "63956011567",
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Kirby",
                    LastName = "Taghoy",
                    Email = "kirby.taghoy@gmail.com",
                    Phone = "639560320053",
                },
            };

            mockCustomerRepo.Setup(repo => repo.IsCustomerExists(It.IsAny<int>())).Returns(Task.FromResult(customers.Any(x => x.Id == customerId)));

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.IsCustomerExists(customerId);

            Assert.Equal(isCustomerExists, result);
        }

        [Fact]
        public async Task UpdateCustomer_Can_Update()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            var dttm = DateTime.Now;
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@yahoo.com",
                Phone = "63956011567",
                CreatedDttm = dttm,
                UpdatedDttm = dttm
            };

            var updateCustomerDto = new UpdateCustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };

            //assuming customer was updated successfully, return 1 from ExecuteUpdateAsync() 
            mockCustomerRepo.Setup(repo => repo.UpdateCustomer(customer)).Returns(Task.FromResult(1));
            mockMapper.Setup(mapper => mapper.Map<Customer>(updateCustomerDto)).Returns(customer);

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.UpdateCustomer(updateCustomerDto);


            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteCustomer_Can_Delete()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();

            //assuming customer was removed successfully, return 1 from ExecuteDeleteAsync() 
            mockCustomerRepo.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).Returns(Task.FromResult(1));

            var customerService = new CustomerService(mockCustomerRepo.Object, mockMapper.Object);
            var result = await customerService.DeleteCustomer(1);

            Assert.Equal(1, result);
        }
    }
}
