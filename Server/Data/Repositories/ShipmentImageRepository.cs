using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class ShipmentImageRepository : RepositoryBase<ShipmentImage>, IShipmentImageRepository
    {
        private readonly ProjectdbContext _imagecontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShipmentImageRepository(ProjectdbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _imagecontext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ShipmentImage>> GetAllImagesAsync()
        {
            try
            {
                var allImages = await _imagecontext.ShipmentImage
                    .Include(i => i.Images)
                    .ToListAsync();

                foreach (var itsImage in allImages)
                {
                    foreach (var image in itsImage.Images)
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
                                throw ex;
                            }
                        }
                    }
                }

                return allImages;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ShipmentImage> GetImagesByPartNumberAsync(int serialNumber)
        {
            try
            {
                var partImages = await _imagecontext.ShipmentImage
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

        public async Task<ShipmentImage> AddImagesAsync(ShipmentImage iTSImage)
        {
            if (iTSImage.Images == null || !iTSImage.Images.Any())
            {
                return iTSImage;
            }

            foreach (var image in iTSImage.Images)
            {
                if (image.Data == null)
                {
                    continue;
                }

                image.ShipmentImageId = iTSImage.Id;
                _imagecontext.Set<MES.Shared.Models.Image>().Add(image);
            }

            _imagecontext.Set<ShipmentImage>().Update(iTSImage);
            await _imagecontext.SaveChangesAsync();

            return iTSImage;
        }

        public async Task<ShipmentImage> UpdateImagesAsync(ShipmentImage iTSImage)
        {
            _imagecontext.Set<ShipmentImage>().Update(iTSImage);
            await _imagecontext.SaveChangesAsync();

            return iTSImage;
        }

        public async Task<bool> DeleteAllImagesByIdPartNumberAsync(int serialNumber)
        {
            try
            {
                var imagesToDelete = await _imagecontext.ShipmentImage
                    .Where(i => i.SerialNumber == serialNumber)
                    .Include(i => i.Images)
                    .ToListAsync();

                if (imagesToDelete == null || imagesToDelete.Count == 0)
                {
                    return false;
                }

                foreach (var image in imagesToDelete.SelectMany(itsImage => itsImage.Images))
                {
                    if (!string.IsNullOrEmpty(image.ImageFilePath) && File.Exists(image.ImageFilePath))
                    {
                        try
                        {
                            File.Delete(image.ImageFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file at path {image.ImageFilePath}: {ex.Message}");
                        }
                    }
                }

                _imagecontext.RemoveRange(imagesToDelete.SelectMany(itsImage => itsImage.Images));
                _imagecontext.RemoveRange(imagesToDelete);

                await _imagecontext.SaveChangesAsync();


                var uploadsFolderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads", serialNumber.ToString());

                if (Directory.Exists(uploadsFolderPath))
                {
                    try
                    {
                        Directory.Delete(uploadsFolderPath, true); // Recursive delete
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting folder at path {uploadsFolderPath}: {ex.Message}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting images: {ex.Message}");
                throw;
            }
        }


      

        #region Old Method

        //public async Task<bool> DeleteAllImagesByIdPartNumberAsync(int partNumber)
        //{
        //    var imagesToDelete = await _imagecontext.ITSImages.Where(i => i.PartNumber == partNumber)
        //       .Include(i => i.Images)
        //        .ToListAsync();


        //    if (imagesToDelete == null )
        //    {
        //        return false;
        //    }

        //    foreach (var image in imagesToDelete)
        //    {
        //        _imagecontext.Remove(image);
        //    }

        //    await _imagecontext.SaveChangesAsync();

        //    return true;
        //}

        public async Task<bool> DeleteImageByIdAsync(int id)
        {
            var imageToDelete = _imagecontext.ShipmentImage.Where(i => i.Id == id)
                .Include(i => i.Images)
                .FirstOrDefaultAsync();

            if (imageToDelete == null)
            {
                return false;
            }

            _imagecontext.Remove(imageToDelete);
            await _imagecontext.SaveChangesAsync();

            return true;
        }
        public async Task<Shared.Models.Image> AddImagesToExistingPartAsync(List<Shared.Models.Image> img)
        {
            if (img == null || img.Count == 0)
            {
                return null;
            }

            try
            {
                _imagecontext.Images.AddRange(img);
                await _imagecontext.SaveChangesAsync();

                return img.LastOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

    }
}
