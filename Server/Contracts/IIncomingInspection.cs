using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MES.Shared.Models.Rotors;

namespace MES.Server.Contracts
{
    public interface IIncomingInspection
    {
        // public int Add(IncomingInspectionDTO IncomingData);
        //Task<IEnumerable<IncomingInspection>> GetAllAsync();
        Task<IncomingInspection?> GetByIdAsync(int id);
        Task<IncomingInspection> AddAsync(IncomingInspection inspection);
        //Task<IncomingInspection?> UpdateAsync(int id, IncomingInspection inspection);
        Task<bool> SerialNumberExistsAsync(string serialNumber);
       // public async Task<IncomingInspection> Add(IncomingInspectionDTO dto)


        Task<IncomingInspection> Add(IncomingInspectionDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
