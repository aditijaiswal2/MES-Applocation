using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface ITypedetailsRepository
    {
        Task<IEnumerable<Typesdetails>> GetWorkCenterAsync();
        Task<bool> AddWorkCenterAsync(Typesdetails typesdetails);
        Task<bool> EditWorkCenterAsync(Typesdetails typesdetails);
        Task DeleteWorkCenter(int id);
    }
}

