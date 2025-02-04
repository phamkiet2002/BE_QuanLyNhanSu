using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ConsoleApp1;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Đang lấy thông tin mạng WiFi...");
            string wifiInfo = GetWiFiInfo();

            // Trích xuất SSID và BSSID
            string ssid = ExtractInfo(wifiInfo, "SSID\\s+:\\s+(.+)");
            string bssid = ExtractInfo(wifiInfo, "BSSID\\s+:\\s+(.+)");

            // Hiển thị thông tin
            Console.WriteLine($"SSID: {ssid}");
            Console.WriteLine($"AP BSSID: {bssid}");

            Console.WriteLine("\nNhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi khi lấy thông tin WiFi: " + e.Message);
        }
    }

    private static string GetWiFiInfo()
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = "wlan show interfaces",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        if (string.IsNullOrEmpty(output))
            throw new Exception("Không nhận được dữ liệu từ netsh.");

        return output;
    }

    private static string ExtractInfo(string input, string pattern)
    {
        var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : "Không xác định";
    }
}
