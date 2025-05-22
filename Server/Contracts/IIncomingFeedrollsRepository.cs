using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IIncomingFeedrollsRepository
    {
        Task<IncomingInspectionFeedRolls?> GetByIdAsync(string serialNumber);
        Task<IncomingInspectionFeedRolls> AddAsync(IncomingInspectionFeedRolls inspection);
        Task<bool> SerialNumberExistsAsync(string serialNumber);     
        Task<bool> DeleteAsync(int id);
    }
}
