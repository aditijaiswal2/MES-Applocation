using MES.Server.Contracts;
using MES.Shared.DTOs.MES.Shared.DTOs.Rotors;
using MES.Shared.Models.Rotors;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class IncomingFeedrollsRepository: IIncomingFeedrollsRepository
    {
        private readonly ProjectdbContext _context;

        public IncomingFeedrollsRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IncomingInspectionFeedRolls?> GetByIdAsync(string serialNumber)
        {
            return await _context.IncomingInspectionFeedRollsdata
                                 .FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task<IncomingInspectionFeedRolls> AddAsync(IncomingInspectionFeedRolls inspection)
        {
            _context.IncomingInspectionFeedRollsdata.Add(inspection);
            await _context.SaveChangesAsync();
            return inspection;
        }

        public async Task<bool> SerialNumberExistsAsync(string serialNumber)
        {
            return await _context.IncomingInspectionFeedRollsdata
                                 .AnyAsync(x => x.SerialNumber == serialNumber);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.IncomingInspectionFeedRollsdata.FindAsync(id);
            if (entity == null) return false;

            _context.IncomingInspectionFeedRollsdata.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

