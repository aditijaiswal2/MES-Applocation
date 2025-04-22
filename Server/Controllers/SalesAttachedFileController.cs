using AutoMapper;
using iTextSharp.text.rtf.graphic;
using MES.Server.Contracts;
using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesAttachedFileController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileRepository _imageRepository;
        private readonly IMapper _mapper;

        public SalesAttachedFileController(IFileRepository imageService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _imageRepository = imageService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("addpi")]
        public async Task<IActionResult> AddImages([FromBody] SalesAttachedFileDto fileDto)
        {
            if (fileDto == null || fileDto.File == null || !fileDto.File.Any())
            {
                return BadRequest("Invalid file data");
            }

            try
            {
                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "SalesFileUploads");
                var partNumberFolder = Path.Combine(uploadsFolderPath, fileDto.SerialNumber.ToString());

                if (!Directory.Exists(partNumberFolder))
                {
                    Directory.CreateDirectory(partNumberFolder);
                }
                var pdfFiles = new List<MES.Shared.Models.Rotors.Filedata>();
                // var images = iTSImageDto.File.Select(imageDto => new MES.Shared.Models.Rotors.Filedata { Data = imageDto.Data }).ToList();

                foreach (var file in fileDto.File)
                {
                    var fileName = $"{Guid.NewGuid()}.pdf";
                    var filePath = Path.Combine(partNumberFolder, fileName);

                    await System.IO.File.WriteAllBytesAsync(filePath, file.Data);
                    pdfFiles.Add(new MES.Shared.Models.Rotors.Filedata
                    {
                        Data = new byte[0], // Avoid storing the whole file content in memory or DB
                        FilePath = filePath
                    });
                       
                   
                }

                var pdfRecord = new SalesAttachedFile()
                {
                    SerialNumber = fileDto.SerialNumber,
                    File = pdfFiles
                };

                var result = await _imageRepository.AddFileAsync(pdfRecord);

                return CreatedAtRoute("GetSalesAttachedFileBySerialNumber", new { serialnumber = result.SerialNumber }, result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{serialnumber}", Name = "GetSalesAttachedFileBySerialNumber")]
        public async Task<IActionResult> GetImagesByPartNumber(string serialnumber)
        {
            var images = await _imageRepository.GetfilesByPartNumberAsync(serialnumber);

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }


        [HttpGet("GFile/{serialnumber}")]
        public async Task<IActionResult> GetImagesserialNumber(string serialnumber)
        {
            var images = await _imageRepository.GetfilesByPartNumberAsync(serialnumber);

            if (images == null)
            {
                return NotFound();
            }

            return Ok(images);
        }
    }
}
