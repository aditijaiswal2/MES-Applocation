using MES.Server.Data.Repositories;
using MES.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode;
using ZXing;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : BaseApiController
    {
        private readonly OtherRepository _rotorsStyleService;

        public OtherController(OtherRepository rotorsStyleService)
        {
            _rotorsStyleService = rotorsStyleService;
        }

        [HttpPost("addwc")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Addworkcenters([FromBody] Other rotors)
        {
            if (rotors == null)
            {
                return BadRequest();
            }
            try
            {
                var createrotorsStyle = await _rotorsStyleService.AddWorkCenterAsync(rotors);

                if (!createrotorsStyle)
                {
                    return BadRequest();
                }

                return Ok("Other added successfully.");

            }
            catch (Exception ex) { return StatusCode(500, $"An error occurred: {ex.Message}"); }
        }

        [HttpPut("editwc")]
        public async Task<IActionResult> Updateworkcenters([FromBody] Other rotors)
        {
            await _rotorsStyleService.EditWorkCenterAsync(rotors);
            return Ok(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteworkcenters(int id)
        {
            await _rotorsStyleService.DeleteWorkCenter(id);
            return Ok(200);
        }

        [HttpGet("getwc")]
        public async Task<ActionResult<IEnumerable<Other>>> Getworkcenters()
        {
            var result = await _rotorsStyleService.GetWorkCenterAsync();
            return Ok(result);
        }

        [HttpPost("wcQR")]
        public IActionResult GenerateLocQRCode([FromBody] Other rotors)
        {
            try
            {
                var qrText = $"Other: {rotors.OtherName}, Other Description: {rotors.Description}, Download-DateTime: {DateTime.Now}";
                var width = 250;
                var height = 250;
                var margin = 0;
                var qrCodeWriter = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.QR_CODE,
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
                        return File(byteArray, "image/png");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
