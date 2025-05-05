using MES.Shared.DTOs;
using MES.Shared.Models;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IShipmentImageRepository 
    {
        Task<IEnumerable<ShipmentImage>> GetAllAsync();
        Task<ShipmentImage?> GetByIdAsync(int id);
        Task<bool> SerialNumberExistsAsync(string serialNumber);
        Task AddIncomingImageAsync(ShipmentImage image);
        Task UpdateIncomingImageAsync(ShipmentImage image);
        Task DeleteIncomingImageAsync(int id);
        Task<ShipmentImage> AddImagesAsync(ShipmentImage wIPForProjectJOB);

        Task<ShipmentImage> GetImagesByDTOAsync(ShipmentImagesDto wIPForProjectJOBDTO);

        Task<ShipmentImage> GetImagesBySerialNumberAsync(string serialnumber);
    }
}
