using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface IMESDelayReasonRepository
    {
        Task<IEnumerable<MESDelayReason>> GetDelayReasonAsync();
        Task<bool> AddDelayReasonAsync(MESDelayReason delayReason);
        Task<bool> EditDelayReasonAsync(MESDelayReason delayReason);
        Task DeleteDelayReason(int id);
    }
}
