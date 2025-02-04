using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.WorkPlace;
public static class Response
{
    public record WorkPlaceResponse(Guid Id, string Name, string Phone, string Email, string Address, DateTime CreatedDate, Domain.Entities.WifiConfig? WifiConfig = null);
}
