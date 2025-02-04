namespace QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;
public interface IWifiCongfigRepository
{
    public string GetWiFiInfo();
    public string ExtractInfo(string input, string pattern);
}
