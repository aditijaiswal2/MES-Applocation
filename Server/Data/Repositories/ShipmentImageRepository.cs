using MES.Server.Contracts;
using MES.Shared.DTOs;
using MES.Shared.Models;
using MES.Shared.Models.Rotors;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class ShipmentImageRepository : IShipmentImageRepository
    {
        private readonly ProjectdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ShipmentImageRepository(ProjectdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddIncomingImageAsync(ShipmentImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            // Ensure the foreign key relationship is correctly established
            foreach (var imageData in image.Images)
            {
                imageData.ShipmentImage = image; // Link each Imagedata object to the parent MaagAmericansImage
            }

            // Add the MaagAmericansImage object to the DbContext
            await _context.ShipmentImage.AddAsync(image);

            // Save changes to persist the data
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SerialNumberExistsAsync(string serialNumber)
        {
            return await _context.FinalInspections.AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task DeleteIncomingImageAsync(int id)
        {
            var image = await _context.FinalInspections.FindAsync(id);
            if (image != null)
            {
                _context.FinalInspections.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShipmentImage>> GetAllAsync()
        {
            return await _context.ShipmentImage.Include(m => m.Images).ToListAsync();
        }


        public async Task<ShipmentImage> AddImagesAsync(ShipmentImage wIPForProjectJOB)
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

                image.ShipmentImageId = wIPForProjectJOB.Id;
                _context.Set<Image>().Add(image);
            }

            _context.Set<ShipmentImage>().Update(wIPForProjectJOB);
            await _context.SaveChangesAsync();

            return wIPForProjectJOB;
        }


        public async Task<ShipmentImage> GetImagesByDTOAsync(ShipmentImagesDto wIPForProjectJOBDTO)
        {
            try
            {
                var partImages = await _context.ShipmentImage
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



        public async Task<ShipmentImage?> GetByIdAsync(int id)
        {
            return await _context.ShipmentImage
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateIncomingImageAsync(ShipmentImage image)
        {
            _context.ShipmentImage.Update(image);
            await _context.SaveChangesAsync();
        }


        public async Task<ShipmentImage> GetImagesBySerialNumberAsync(string serialNumber)
        {
            try
            {
                var partImages = await _context.ShipmentImage
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
    }
}

