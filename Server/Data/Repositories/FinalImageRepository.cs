using iTextSharp.text.rtf.graphic;
using MES.Server.Contracts;
using MES.Server.Data;
using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class FinalRepository : IFinalInspectionImages
    {
        private readonly ProjectdbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FinalRepository(ProjectdbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddIncomingImageAsync(FinalInspection image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            // Ensure the foreign key relationship is correctly established
            foreach (var imageData in image.Images)
            {
                imageData.FinalImages = image; // Link each Imagedata object to the parent MaagAmericansImage
            }

            // Add the MaagAmericansImage object to the DbContext
            await _context.finalInspections.AddAsync(image);

            // Save changes to persist the data
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SerialNumberExistsAsync(string serialNumber)
        {
            return await _context.finalInspections.AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task DeleteIncomingImageAsync(int id)
        {
            var image = await _context.finalInspections.FindAsync(id);
            if (image != null)
            {
                _context.finalInspections.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FinalInspection>> GetAllAsync()
        {
            return await _context.finalInspections.Include(m => m.Images).ToListAsync();
        }


        public async Task<FinalInspection> AddImagesAsync(FinalInspection wIPForProjectJOB)
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
                _context.Set<FinalImagedata>().Add(image);
            }

            _context.Set<FinalInspection>().Update(wIPForProjectJOB);
            await _context.SaveChangesAsync();

            return wIPForProjectJOB;
        }


        public async Task<FinalInspection> GetImagesByDTOAsync(FinalInspectionImageDTO wIPForProjectJOBDTO)
        {
            try
            {
                var partImages = await _context.finalInspections
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



        public async Task<FinalInspection?> GetByIdAsync(int id)
        {
            return await _context.finalInspections
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateIncomingImageAsync(FinalInspection image)
        {
            _context.finalInspections.Update(image);
            await _context.SaveChangesAsync();
        }


        public async Task<FinalInspection> GetImagesBySerialNumberAsync(string serialNumber)
        {
            try
            {
                var partImages = await _context.finalInspections
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

