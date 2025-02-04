namespace wifi;
public class WorkPlaceWiFi
{
    public int Id { get; private set; }
    public int WorkPlaceId { get; private set; }
    public string SSID { get; private set; }
    public string BSSID { get; private set; }
    public string IPRange { get; private set; }
    public string Note { get; private set; }

    // Constructor
    public WorkPlaceWiFi(int workPlaceId, string ssid, string bssid, string ipRange, string note)
    {
        WorkPlaceId = workPlaceId;
        SSID = ssid;
        BSSID = bssid;
        IPRange = ipRange;
        Note = note;
    }

    // Update Wi-Fi Configuration
    public void UpdateWiFiConfiguration(string ssid, string bssid, string ipRange, string note)
    {
        SSID = ssid;
        BSSID = bssid;
        IPRange = ipRange;
        Note = note;
    }
}
