namespace QuanLyNhanSu.Contract.Service.V1.Account;
public static class Response
{
    public record RegisterResponse(string UserName, string Email, string Password);
    public record LoginResponse(string UserName, string Password);
    public record ChangePasswordResponese(string Password, string NewPassword);
}
