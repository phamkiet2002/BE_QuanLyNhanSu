using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.WifiConfig;
public static class Query
{
    public record GetWifiConfigQuery() : IQuery<Response.WifiConfigInforResponse>;
}
