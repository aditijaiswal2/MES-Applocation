using MES.Server.Data.Repositories;
using MES.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode;
using ZXing;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MESWorkCentersController : BaseApiController
    {
        private readonly MESWorkCentersRepository _workcenterService;
        public MESWorkCentersController(MESWorkCentersRepository workcenterService)
        {
            _workcenterService = workcenterService;
        }

        [HttpPost("addwc")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Addworkcenters([FromBody] MESWorkcenters workcenters)
        {
            if (workcenters == null)
            {
                return BadRequest();
            }
            try
            {
                var createworkcenters = await _workcenterService.AddWorkCenterAsync(workcenters);

                if (!createworkcenters)
                {
                    return BadRequest();
                }

                return Ok("WorkCenter added successfully.");

            }
            catch (Exception ex) { return StatusCode(500, $"An error occurred: {ex.Message}"); }
        }


        [HttpPut("editwc")]
        public async Task<IActionResult> Updateworkcenters([FromBody] MESWorkcenters workcenters)
        {
            await _workcenterService.EditWorkCenterAsync(workcenters);
            return Ok(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteworkcenters(int id)
        {
            await _workcenterService.DeleteWorkCenter(id);
            return Ok(200);
        }


        [HttpGet("getwc")]
        public async Task<ActionResult<IEnumerable<MESWorkcenters>>> Getworkcenters()
        {
            var result = await _workcenterService.GetWorkCenterAsync();
            return Ok(result);
        }


        [HttpPost("wcQR")]
        public IActionResult GenerateLocQRCode([FromBody] MESWorkcenters workcenters)
        {
            try
            {
                var qrText = $"Workcenter: {workcenters.Workcenters}, Workcenter Description: {workcenters.Description}, Download-DateTime: {DateTime.Now}";
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
