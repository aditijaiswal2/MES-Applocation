using iTextSharp.text.rtf.graphic;
using MES.Server.Contracts;
using MES.Shared.Models.Rotors;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class FileRepository : RepositoryBase<SalesAttachedFile>, IFileRepository
    {
        private readonly ProjectdbContext _imagecontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileRepository(ProjectdbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _imagecontext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<SalesAttachedFile> AddFileAsync(SalesAttachedFile salesAttachedFile)
        {
            if (salesAttachedFile.File == null || !salesAttachedFile.File.Any())
            {
                return salesAttachedFile;
            }

            foreach (var image in salesAttachedFile.File)
            {
                if (image.Data == null)
                {
                    continue;
                }

                image.SalesAttachedFileId = salesAttachedFile.Id;
                _imagecontext.Set<MES.Shared.Models.Rotors.Filedata>().Add(image);
            }

            _imagecontext.Set<SalesAttachedFile>().Update(salesAttachedFile);
            await _imagecontext.SaveChangesAsync();

            return salesAttachedFile;
        }


        public async Task<SalesAttachedFile> GetfilesByPartNumberAsync(string serialnumber)
        {
            try
            {
                var partImages = await _imagecontext.SalesAttachedFile
                                    .Where(i => i.SerialNumber == serialnumber)
                                    .Include(i => i.File)
                                    .FirstOrDefaultAsync();

                if (partImages != null)
                {
                    foreach (var image in partImages.File)
                    {
                        if (!string.IsNullOrEmpty(image.FilePath) && File.Exists(image.FilePath))
                        {
                            try
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, image.FilePath);
                                image.Data = await File.ReadAllBytesAsync(imagePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error reading file at path {image.FilePath}: {ex.Message}");
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
