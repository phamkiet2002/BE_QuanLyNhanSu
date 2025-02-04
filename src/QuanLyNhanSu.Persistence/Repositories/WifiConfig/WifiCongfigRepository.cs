using System.Diagnostics;
using System.Text.RegularExpressions;
using QuanLyNhanSu.Domain.Abstractions.Repositories.WifiConfig;

namespace QuanLyNhanSu.Persistence.Repositories.WifiConfig;
public class WifiCongfigRepository : IWifiCongfigRepository
{
    public string ExtractInfo(string input, string pattern)
    {
        var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : "Không xác định";
    }

    public string GetWiFiInfo()
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
}
