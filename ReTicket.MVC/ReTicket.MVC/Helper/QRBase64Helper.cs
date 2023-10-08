using QRCoder;
using ReTicket.Domain.Models;
using System.CodeDom;

namespace ReTicket.MVC.Helper
{
    public static class QRBase64Helper
    {
        private static QRCodeGenerator _qr;

        static QRBase64Helper()
        {
            _qr = new QRCodeGenerator();
        }

        public static string Generate(Guid key)
        {
            var qrData = _qr.CreateQrCode(key.ToString(), QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrData);
            var qrCodeImage = qrCode.GetGraphic(2);
            string base64QrCode;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                qrCodeImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bytes = memoryStream.ToArray();
                base64QrCode = Convert.ToBase64String(bytes);
            }

            return base64QrCode;
        }
    }
}
