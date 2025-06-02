using iTextSharp.text.rtf.graphic;
using MES.Shared.DTOs;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IFinalInspectionImages
    {
        Task<IEnumerable<FinalInspection>> GetAllAsync();
        Task<FinalInspection?> GetByIdAsync(int id);
        Task<bool> SerialNumberExistsAsync(string serialNumber);
        Task AddIncomingImageAsync(FinalInspection image);
        Task UpdateIncomingImageAsync(FinalInspection image);
        Task DeleteIncomingImageAsync(string SerialNumber);
        Task<FinalInspection> AddImagesAsync(FinalInspection wIPForProjectJOB);

        Task<FinalInspection> GetImagesByDTOAsync(FinalInspectionImageDTO wIPForProjectJOBDTO);

        Task<FinalInspection> GetImagesBySerialNumberAsync(string serialnumber);

        Task<List<FinalInspection>> GetImagesBySerialNumberAsyncforsales(string serialNumber);




    }
}
