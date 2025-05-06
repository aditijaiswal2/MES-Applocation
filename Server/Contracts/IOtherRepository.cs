using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IOtherRepository
    {
        Task<IEnumerable<Other>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(Other rotors);
        Task<bool> EditWorkCenterAsync(Other rotors);
        Task DeleteWorkCenter(int id);
    }
}
