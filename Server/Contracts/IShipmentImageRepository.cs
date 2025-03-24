using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IShipmentImageRepository : IRepositoryBase<ShipmentImage>
    {
        Task<IEnumerable<ShipmentImage>> GetAllImagesAsync();
        Task<ShipmentImage> GetImagesByPartNumberAsync(int id);
        Task<ShipmentImage> AddImagesAsync(ShipmentImage shipmentImage);

        Task<Image> AddImagesToExistingPartAsync(List<Image> img);
        Task<ShipmentImage> UpdateImagesAsync(ShipmentImage shipmentImage);
        Task<bool> DeleteImageByIdAsync(int id);
        Task<bool> DeleteAllImagesByIdPartNumberAsync(int serialNumber);
    }
}
