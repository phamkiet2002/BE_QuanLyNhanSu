using Microsoft.VisualBasic;

namespace QuanLyNhanSu.Contract.Service.V1.Level;
public static class Response
{
    public record LevelResponse(Guid Id, string Name, string Description, DateTime CreatedDate);
}
