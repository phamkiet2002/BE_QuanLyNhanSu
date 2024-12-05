using AutoMapper;
using QuanLyNhanSu.Domain.Entities;

namespace QuanLyNhanSu.Application.Mapper;
public class AttendanceMapper : Profile
{
    public AttendanceMapper()
    {
        // Attendance
        CreateMap<Attendance, Contract.Service.V1.Attendance.Response.AttendanceResponse>();
    }
}
