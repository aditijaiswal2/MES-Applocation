﻿using MES.Server.Contracts;
using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IncomingImagesController(IImageRepository imageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _imageRepository = imageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<IncomingImages>>> GetAll()
        {
            var images = await _imageRepository.GetAllAsync();
            return Ok(images);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IncomingImages>> GetById(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
        }

        [HttpPost("maagdata")]
        public async Task<ActionResult> Add(IncomingImages image)
        {
            // Set the MAAGImage navigation property for each Imagedata object
            foreach (var imageData in image.Images)
            {
                imageData.IncomingImages = image;  // Link the imageData to the parent MaagAmericansImage
                imageData.IncomingImageId = image.Id;
            }

            await _imageRepository.AddIncomingImageAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = image.Id }, image);
        }

        [HttpGet("GetCurrentUsername")]
        public IActionResult GetCurrentUsername()
        {
            try
            {
                var username = Environment.UserName; // Fetch the system username.
                return Ok(username);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, IncomingImages image)
        {
            if (id != image.Id)
                return BadRequest();

            await _imageRepository.UpdateIncomingImageAsync(image);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _imageRepository.DeleteIncomingImageAsync(id);
            return NoContent();
        }

        [HttpPost("addbi")]
        public async Task<IActionResult> AddImages([FromBody] IncomingInspectionImageDTO IncomingImagesDTO)
        {
            if (IncomingImagesDTO == null || IncomingImagesDTO.Images == null || !IncomingImagesDTO.Images.Any())
            {
                return BadRequest("Invalid image data");
            }

            try
            {
                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "IncomingInspection");
                //var partNumberFolder = Path.Combine(uploadsFolderPath, bOMImageDto.ToString());
                var partNumberFolder = Path.Combine(uploadsFolderPath, $"{IncomingImagesDTO.SerialNumber}");

                if (!Directory.Exists(partNumberFolder))
                {
                    Directory.CreateDirectory(partNumberFolder);
                }

                var images = IncomingImagesDTO.Images.Select(imageDto => new Imagedata { Data = imageDto.Data }).ToList();

                foreach (var image in images)
                {
                    var fileName = $"{Guid.NewGuid()}.png";
                    var filePath = Path.Combine(partNumberFolder, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, image.Data);

                    image.Data = new byte[0];

                    image.ImageFilePath = filePath;
                }

                var bOMImage = new IncomingImages
                {
                    SerialNumber = IncomingImagesDTO.SerialNumber,
                   // Project = projectJobImagesDTO.Project,
                    Images = images
                };

                var result = await _imageRepository.AddImagesAsync(bOMImage);

                return CreatedAtAction(nameof(GetImagesByDTO), new { wipDTO = new IncomingInspectionImageDTO { SerialNumber = result.SerialNumber } }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("gd")]
        public async Task<IActionResult> GetImagesByDTO(IncomingInspectionImageDTO wIPForProjectJOBDTO)
        {
            var images = await _imageRepository.GetImagesByDTOAsync(wIPForProjectJOBDTO);

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }


    }
}

