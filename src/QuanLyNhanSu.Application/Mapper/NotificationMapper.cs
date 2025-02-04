using AutoMapper;

namespace QuanLyNhanSu.Application.Mapper;
public class NotificationMapper : Profile
{
    public NotificationMapper()
    {
        CreateMap<Domain.Entities.Notification, Contract.Service.V1.Notification.Response.NotificationResponse>();
    }
}
