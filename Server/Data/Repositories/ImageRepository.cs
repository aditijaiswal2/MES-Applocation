using iTextSharp.text.rtf.graphic;
using MES.Server.Contracts;
using MES.Server.Data;
using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ProjectdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ImageRepository(ProjectdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddIncomingImageAsync(IncomingImages image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            // Ensure the foreign key relationship is correctly established
            foreach (var imageData in image.Images)
            {
                imageData.IncomingImages = image; // Link each Imagedata object to the parent MaagAmericansImage
            }

            // Add the MaagAmericansImage object to the DbContext
            await _context.IncomingImages.AddAsync(image);

            // Save changes to persist the data
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncomingImageAsync(string serialNumber)
        {
            var images = await _context.IncomingImages
                .Where(img => img.SerialNumber == serialNumber)
                .ToListAsync();

            if (images.Any())
            {
                _context.IncomingImages.RemoveRange(images);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncomingImages>> GetAllAsync()
        {
            return await _context.IncomingImages.Include(m => m.Images).ToListAsync();
        }


        public async Task<IncomingImages> AddImagesAsync(IncomingImages wIPForProjectJOB)
        {
            if (wIPForProjectJOB.Images == null || !wIPForProjectJOB.Images.Any())
            {
                return wIPForProjectJOB;
            }

            foreach (var image in wIPForProjectJOB.Images)
            {
                if (image.Data == null)
                {
                    continue;
                }

                image.IncomingImageId = wIPForProjectJOB.Id;
                _context.Set<Imagedata>().Add(image);
            }

            _context.Set<IncomingImages>().Update(wIPForProjectJOB);
            await _context.SaveChangesAsync();

            return wIPForProjectJOB;
        }


        public async Task<IncomingImages> GetImagesByDTOAsync(IncomingInspectionImageDTO wIPForProjectJOBDTO)
        {
            try
            {
                var partImages = await _context.IncomingImages
                                    .Where(i => i.SerialNumber == wIPForProjectJOBDTO.SerialNumber)
                                    .Include(i => i.Images)
                                    .FirstOrDefaultAsync();

                if (partImages != null)
                {
                    foreach (var image in partImages.Images)
                    {
                        if (!string.IsNullOrEmpty(image.ImageFilePath) && File.Exists(image.ImageFilePath))
                        {
                            try
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, image.ImageFilePath);
                                image.Data = await File.ReadAllBytesAsync(imagePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error reading file at path {image.ImageFilePath}: {ex.Message}");
                                throw;
                            }
                        }
                    }
                }

                return partImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }



        public async Task<IncomingImages?> GetByIdAsync(int id)
        {
            return await _context.IncomingImages
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateIncomingImageAsync(IncomingImages image)
        {
            _context.IncomingImages.Update(image);
            await _context.SaveChangesAsync();
        }



        public async Task<IncomingImages> GetImagesBySerialNumberAsync(string serialNumber)
        {
            try
            {
                var partImages = await _context.IncomingImages
                                    .Where(i => i.SerialNumber == serialNumber)
                                    .Include(i => i.Images)
                                    .FirstOrDefaultAsync();

                if (partImages != null)
                {
                    foreach (var image in partImages.Images)
                    {
                        if (!string.IsNullOrEmpty(image.ImageFilePath) && File.Exists(image.ImageFilePath))
                        {
                            try
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, image.ImageFilePath);
                                image.Data = await File.ReadAllBytesAsync(imagePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error reading file at path {image.ImageFilePath}: {ex.Message}");
                                throw;
                            }
                        }
                    }
                }

                return partImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        public async Task<List<IncomingImages>> GetImagesBySerialNumberAsyncforsales(string serialNumber)
        {
            try
            {
                var partImages = await _context.IncomingImages
                    .Where(i => i.SerialNumber == serialNumber)
                    .Include(i => i.Images)
                    .ToListAsync();

                if (partImages != null && partImages.Any())
                {
                    foreach (var partImage in partImages)
                    {
                        foreach (var image in partImage.Images)
                        {
                            if (!string.IsNullOrEmpty(image.ImageFilePath) && File.Exists(image.ImageFilePath))
                            {
                                try
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, image.ImageFilePath);
                                    image.Data = await File.ReadAllBytesAsync(imagePath);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error reading file at path {image.ImageFilePath}: {ex.Message}");
                                    throw;
                                }
                            }
                        }
                    }
                }

                return partImages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

}
}