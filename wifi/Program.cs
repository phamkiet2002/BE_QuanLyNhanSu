
using System.Management;

using System;

namespace wifi;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Đang lấy thông tin mạng WiFi...");

            // Truy vấn thông tin WiFi
            string query = "SELECT * FROM MSNdis_80211_BSSIList";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\wmi", query);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                Console.WriteLine("SSID: " + GetSSID(queryObj["Ndis80211SsId"]));
                Console.WriteLine("Tín hiệu (Signal Strength): " + queryObj["Ndis80211Rssi"] + " dBm");
            }

            Console.WriteLine("\nHoàn tất.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi khi lấy thông tin WiFi: " + e.Message);
        }


        private static string GetSSID(object ssidObject)
        {
            if (ssidObject == null)
                return "Không xác định";

            byte[] ssidBytes = (byte[])ssidObject;
            return System.Text.Encoding.UTF8.GetString(ssidBytes);
        }
    }
}
