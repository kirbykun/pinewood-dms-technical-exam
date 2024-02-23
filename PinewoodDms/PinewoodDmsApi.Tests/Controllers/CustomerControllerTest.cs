using Microsoft.AspNetCore.Mvc;
using Moq;
using PinewoodDmsApi.Controllers;
using PinewoodDmsApi.Dtos;
using PinewoodDmsApi.Services;

namespace PinewoodDmsApi.Tests.Controllers
{
    public class CustomerControllerTest
    {
        [Fact]

        public async Task GetCustomers_Should_Return_Not_Found()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.GetCustomers()).Returns(Task.FromResult(new List<CustomerDTO>()));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.GetCustomers();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomers_Should_Return_Customer_List()
        {
            var mockService = new Mock<ICustomerService>();
            var mockCustomers = new List<CustomerDTO>
            {
               new CustomerDTO
                {
                    Id = 1,
                    FirstName = "Jhon",
                    LastName = "Ambrad",
                    Email = "jhon.ambrad@gmail.com",
                    Phone = "63956011567",
                },
            };
            mockService.Setup(x => x.GetCustomers()).Returns(Task.FromResult(mockCustomers));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.GetCustomers();

            var objResult = result as ObjectResult;
            Assert.NotNull(objResult);

            var customers = objResult?.Value as List<CustomerDTO>;
            Assert.True(customers?.Count > 0);
        }

        [Fact]
        public async Task GetCustomer_Should_Return_Not_Found()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.GetCustomer(It.IsAny<int>())).Returns(Task.FromResult<CustomerDTO?>(null));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.GetCustomer(It.IsAny<int>());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomer_Should_Return_Customer()
        {
            var mockService = new Mock<ICustomerService>();
            var mockCustomer = new CustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };
            mockService.Setup(x => x.GetCustomer(It.IsAny<int>())).Returns(Task.FromResult<CustomerDTO?>(mockCustomer));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.GetCustomer(1);

            var objResult = result as ObjectResult;
            Assert.NotNull(objResult?.Value as CustomerDTO);
        }

        [Fact]
        public async Task InsertCustomer_Should_Return_Bad_Request()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.InsertCustomer(It.IsAny<InsertCustomerDTO>())).Returns(Task.FromResult(0));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.InsertCustomer(It.IsAny<InsertCustomerDTO>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task InsertCustomer_Should_Insert_Successfully()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.InsertCustomer(It.IsAny<InsertCustomerDTO>())).Returns(Task.FromResult(1));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.InsertCustomer(It.IsAny<InsertCustomerDTO>());

            var objResult = result as OkResult;
            Assert.NotNull(objResult);
        }

        [Fact]
        public async Task UpdateCustomer_Should_Return_Notfound()
        {
            var mockService = new Mock<ICustomerService>();
            var mockUpdateCustomer = new UpdateCustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };
            mockService.Setup(x => x.IsCustomerExists(It.IsAny<int>())).Returns(Task.FromResult(false));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.UpdateCustomer(mockUpdateCustomer);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_Should_Return_BadRequest()
        {
            var mockService = new Mock<ICustomerService>();
            var mockUpdateCustomer = new UpdateCustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };
            mockService.Setup(x => x.IsCustomerExists(It.IsAny<int>())).Returns(Task.FromResult(true));
            mockService.Setup(x => x.UpdateCustomer(It.IsAny<UpdateCustomerDTO>())).Returns(Task.FromResult(0));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.UpdateCustomer(mockUpdateCustomer);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_Should_Update_Successfully()
        {
            var mockService = new Mock<ICustomerService>();
            var mockUpdateCustomer = new UpdateCustomerDTO
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Ambrad",
                Email = "jhon.ambrad@gmail.com",
                Phone = "63956011567",
            };
            mockService.Setup(x => x.IsCustomerExists(It.IsAny<int>())).Returns(Task.FromResult(true));
            mockService.Setup(x => x.UpdateCustomer(It.IsAny<UpdateCustomerDTO>())).Returns(Task.FromResult(1));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.UpdateCustomer(mockUpdateCustomer);

            var objResult = result as OkResult;
            Assert.NotNull(objResult);
        }


        [Fact]
        public async Task DeleteCustomer_Should_Return_Bad_Request()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Returns(Task.FromResult(0));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.DeleteCustomer(It.IsAny<int>());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_Should_Delete_Successfully()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Returns(Task.FromResult(1));

            var controller = new CustomerController(mockService.Object);
            var result = await controller.DeleteCustomer(It.IsAny<int>());

            var objResult = result as OkResult;
            Assert.NotNull(objResult);
        }
    }
}
