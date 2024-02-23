using AutoMapper;
using PinewoodDmsApi.Dtos;
using PinewoodDmsApi.Models;

namespace PinewoodDmsApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<Customer, CustomerDTO>();
            CreateMap<InsertCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
        }
    }
}
