using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IMESWorkCentersRepository
    {
        Task<IEnumerable<MESWorkcenters>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(MESWorkcenters workcenters);
        Task<bool> EditWorkCenterAsync(MESWorkcenters workcenters);
        Task DeleteWorkCenter(int id);
    }
}
