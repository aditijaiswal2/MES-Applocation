using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IImageRepository
    {
       Task<IEnumerable<IncomingImages>> GetAllAsync();
        Task<IncomingImages?> GetByIdAsync(int id);
        Task AddIncomingImageAsync(IncomingImages image);
        Task UpdateIncomingImageAsync(IncomingImages image);
        Task DeleteIncomingImageAsync(int id);
        Task<IncomingImages> AddImagesAsync(IncomingImages wIPForProjectJOB);

        Task<IncomingImages> GetImagesByDTOAsync(IncomingInspectionImageDTO wIPForProjectJOBDTO);




    }
}
