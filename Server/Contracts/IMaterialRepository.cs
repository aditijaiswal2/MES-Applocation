using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Materials>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(Materials rotors);
        Task<bool> EditWorkCenterAsync(Materials rotors);
        Task DeleteWorkCenter(int id);
    }
}
