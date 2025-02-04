namespace QuanLyNhanSu.Contract.Service.V1.WifiConfig;
public static class Response
{
    public record WifiConfigResponse(Guid Id, string SSID, string BSSID, Guid WorkPlaceId);

    public record WifiConfigInforResponse(string SSID, string BSSID);
}
