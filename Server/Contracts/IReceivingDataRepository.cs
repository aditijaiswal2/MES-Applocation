using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IReceivingDataRepository
    {
        Task<IEnumerable<Receiving>> GetAllAsync(); // Get all records
        Task<Receiving> GetByIdAsync(int id); // Get by ID
        Task<Receiving> CreateAsync(Receiving data); // Create new record
        Task<bool> UpdateAsync(Receiving data); // Update record
        Task<bool> DeleteAsync(int id); // Delete record
    }
}
