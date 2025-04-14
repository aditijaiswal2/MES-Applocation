using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.SkiaSharp;
using SkiaSharp;
using System;
using System.IO;
using MES.Shared.Models;
using ZXing.QrCode;
using MES.Shared.Models.Rotors;
using static MES.Client.Dialog.Rotors.PreviewDialog;
using MES.Shared.DTOs;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        [HttpPost("locQR")]
        public IActionResult GenerateLocQRCode([FromBody] Receiving receive)
        {
            try
            {


                var qrText = $"Serial Number: {receive.SerialNumber},Module: {receive.SelectedOption}, Customer: {receive.Customer}, Date: {receive.Date}";
                var width = 250;
                var height = 250;
                var margin = 0;
                var qrCodeWriter = new BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = height,
                        Width = width,
                        Margin = margin
                    }
                };
                var pixelData = qrCodeWriter.Write(qrText);

                using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        var byteArray = ms.ToArray();
                        // Return the QR code image to the client
                        return File(byteArray, "image/png");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //[HttpPost("IncomlocQR")]
        //public IActionResult GenerateLocQRCodelog([FromBody] QRRequestDTO request)
        //{
        //    try
        //    {


        //        var qrText = $"Serial Number: {request.SerialNumber},Module: {request.Module}, Customer: {request.Customer}, Date: {request.Date}";
        //        var width = 250;
        //        var height = 250;
        //        var margin = 0;
        //        var qrCodeWriter = new BarcodeWriterPixelData
        //        {
        //            Format = ZXing.BarcodeFormat.QR_CODE,
        //            Options = new QrCodeEncodingOptions
        //            {
        //                Height = height,
        //                Width = width,
        //                Margin = margin
        //            }
        //        };
        //        var pixelData = qrCodeWriter.Write(qrText);

        //        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        //        {
        //            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //            try
        //            {
        //                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
        //            }
        //            finally
        //            {
        //                bitmap.UnlockBits(bitmapData);
        //            }

        //            using (var ms = new MemoryStream())
        //            {
        //                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                var byteArray = ms.ToArray();
        //                // Return the QR code image to the client
        //                return File(byteArray, "image/png");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
    }
}
