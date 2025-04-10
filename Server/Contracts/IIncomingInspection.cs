using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IIncomingInspection
    {

        Task<IEnumerable<IncomingInspection>> GetAllAsync();
        Task<IncomingInspection?> GetByIdAsync(int id);
        Task<IncomingInspection> AddAsync(IncomingInspection inspection);
        Task<IncomingInspection?> UpdateAsync(int id, IncomingInspection inspection);
        Task<bool> DeleteAsync(int id);
    }
}
