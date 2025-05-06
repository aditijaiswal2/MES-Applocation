using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface ISaddlePartNumberRepository
    {
        Task<IEnumerable<SaddlepartNumber>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(SaddlepartNumber rotors);
        Task<bool> EditWorkCenterAsync(SaddlepartNumber rotors);
        Task DeleteWorkCenter(int id);
    }
}
