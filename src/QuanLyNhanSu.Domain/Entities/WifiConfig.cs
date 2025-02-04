using System.Text.Json.Serialization;
using QuanLyNhanSu.Domain.Abstractions.Entities;

namespace QuanLyNhanSu.Domain.Entities;
public class WifiConfig : DomainEntity<Guid>
{
    public string SSID { get; set; }
    public string BSSID { get; set; }

    public Guid? WorkPlaceId { get; set; }
    [JsonIgnore]
    public virtual WorkPlace WorkPlace { get; set; }

    public WifiConfig() { }

    public WifiConfig(Guid id, string ssid, string bssid, Guid workPlaceId)
    {
        Id = id;
        SSID = ssid;
        BSSID = bssid;
        WorkPlaceId = workPlaceId;
    }

    public static WifiConfig CreateWifiConfig(Guid id, string ssid, string bssid, Guid workPlaceId)
    {
        return new WifiConfig(id, ssid, bssid, workPlaceId);
    }

    public void UpdateWifiConfig(string ssid, string bssid)
    {
        SSID = ssid;
        BSSID = bssid;
    }
}
