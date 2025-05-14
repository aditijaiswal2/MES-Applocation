using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface INewBoxRequiredNumberRepository
    {
        Task<IEnumerable<NewBoxRequiredNumber>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(NewBoxRequiredNumber rotors);
        Task<bool> EditWorkCenterAsync(NewBoxRequiredNumber rotors);
        Task DeleteWorkCenter(int id);
    }
}
