using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IIncomingInspection
    {

        Task<IEnumerable<IncomingInspection>> GetAllAsync();
        Task<IncomingInspection> GetByIdAsync(int id);
        Task AddAsync(IncomingInspection order);
        Task UpdateAsync(IncomingInspection order);
        Task DeleteAsync(int id);
    }
}
