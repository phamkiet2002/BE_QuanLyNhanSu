using AutoMapper;
using QuanLyNhanSu.Contract.Service.V1.PayRoll;

namespace QuanLyNhanSu.Application.Mapper;
public class PayRollMapper : Profile
{
    public PayRollMapper()
    {
        CreateMap<Domain.Entities.PayRoll, Contract.Service.V1.PayRoll.Response.PayRollResponse>().ReverseMap();
    }
}
