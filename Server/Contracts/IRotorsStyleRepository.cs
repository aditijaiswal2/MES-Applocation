using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IRotorsStyleRepository
    {
        Task<IEnumerable<RotorsStyle>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(RotorsStyle rotors);
        Task<bool> EditWorkCenterAsync(RotorsStyle rotors);
        Task DeleteWorkCenter(int id);
    }
}

