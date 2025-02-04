using QuanLyNhanSu.Contract.Abstractions.Message;

namespace QuanLyNhanSu.Contract.Service.V1.WifiConfig;
public static class Command
{
    public record CreateWifiConfigCommand(string SSID, string BSSID, Guid WorkPlaceId) : ICommand;

    public record UpdateWifiConfigCommand(Guid Id, string SSID, string BSSID) : ICommand;
}
