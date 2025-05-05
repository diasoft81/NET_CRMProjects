using AutoMapper;
using CRM.Application.DTOs;
using CRM.Domain.Entities;

namespace CRM.Application.Mappings;

public class SalesPersonProfile : Profile
{
    public SalesPersonProfile()
    {
        CreateMap<SalesPerson, SalesPersonDto>().ReverseMap();
    }
}