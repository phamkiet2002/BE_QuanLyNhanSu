using QRCoder;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace VietQR;

public class Program
{
    public static void Main(string[] args)
    {
        string bankAccount = "7707205113133";
        string bankName = "Agribank";
        //  https://img.vietqr.io/image/Agribank-7707205113133-qr_only.png?accountName=phamtuankiet
        string quickQR = $"https://img.vietqr.io/image/{bankName}-{bankAccount}";

        Program program = new Program();
        string base64QRCode = program.GenerateQRCode(quickQR);

        Console.WriteLine("Base64 QR Code:");
        Console.WriteLine(base64QRCode);
    }

    public string GenerateQRCode(string url)
    {
        using (var qrGenerator = new QRCodeGenerator())
        {
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var pngBytes = qrCode.GetGraphic(20);
            var base64QRCode = Convert.ToBase64String(pngBytes);
            return base64QRCode;
        }
    }
}
