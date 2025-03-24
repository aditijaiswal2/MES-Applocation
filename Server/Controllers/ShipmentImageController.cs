using AutoMapper;
using MES.Server.Contracts;
using MES.Shared.DTOs;
using MES.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentImageController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShipmentImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ShipmentImageController(IShipmentImageRepository imageService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _imageRepository = imageService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("gall")]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var allImages = await _imageRepository.GetAllImagesAsync();
                return Ok(allImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{partNumber}")]
        public async Task<IActionResult> GetImagesByPartNumber(int partNumber)
        {
            var images = await _imageRepository.GetImagesByPartNumberAsync(partNumber);

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }

        [HttpPost("addpi")]
        public async Task<IActionResult> AddImages([FromBody] ShipmentImagesDto iTSImageDto)
        {
            if (iTSImageDto == null || iTSImageDto.Images == null || !iTSImageDto.Images.Any())
            {
                return BadRequest("Invalid image data");
            }

            try
            {
                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
                var partNumberFolder = Path.Combine(uploadsFolderPath, iTSImageDto.SerialNumber.ToString());

                if (!Directory.Exists(partNumberFolder))
                {
                    Directory.CreateDirectory(partNumberFolder);
                }

                var images = iTSImageDto.Images.Select(imageDto => new MES.Shared.Models.Image { Data = imageDto.Data }).ToList();

                foreach (var image in images)
                {
                    var fileName = $"{Guid.NewGuid()}.png";
                    var filePath = Path.Combine(partNumberFolder, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, image.Data);

                    image.Data = new byte[0];

                    image.ImageFilePath = filePath;
                }

                var iTSImages = new ShipmentImage()
                {
                    SerialNumber = iTSImageDto.SerialNumber,
                    Module = iTSImageDto.Module,
                    Images = images
                };

                var result = await _imageRepository.AddImagesAsync(iTSImages);

                return CreatedAtAction(nameof(GetImagesByPartNumber), new { partNumber = result.SerialNumber }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("upi")]
        public async Task<IActionResult> UpdateImages([FromBody] ShipmentImagesDto iTSImageDto)
        {
            if (iTSImageDto == null || iTSImageDto.Images == null || !iTSImageDto.Images.Any())
            {
                return BadRequest("Invalid image data or ID mismatch");
            }

            try
            {
                var existingITSImage = await _imageRepository.GetImagesByPartNumberAsync(iTSImageDto.SerialNumber);

                if (existingITSImage == null)
                {
                    return NotFound($"ITSImage with part number {iTSImageDto.SerialNumber} not found");
                }

                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", iTSImageDto.SerialNumber.ToString());

                if (Directory.Exists(uploadsFolderPath))
                {
                    var existingFiles = Directory.EnumerateFiles(uploadsFolderPath);
                    foreach (var file in existingFiles)
                    {
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    return NotFound($"Folder for part number {iTSImageDto.SerialNumber} does not exist");
                }

                existingITSImage.Images.Clear();

                existingITSImage.Images.AddRange(iTSImageDto.Images.Select(image => new MES.Shared.Models.Image { Data = image.Data }));

                foreach (var image in existingITSImage.Images)
                {
                    var fileName = $"{Guid.NewGuid()}.png";
                    var filePath = Path.Combine(uploadsFolderPath, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, image.Data);

                    image.Data = new byte[0];

                    image.ImageFilePath = filePath;
                }

                var result = await _imageRepository.UpdateImagesAsync(existingITSImage);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("part/{partNumber}")]
        public async Task<IActionResult> DeleteAllImagesByPartNumber(int partNumber)
        {
            var result = await _imageRepository.DeleteAllImagesByIdPartNumberAsync(partNumber);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
