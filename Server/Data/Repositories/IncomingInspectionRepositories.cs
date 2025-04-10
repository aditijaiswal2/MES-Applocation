using MES.Server.Contracts;
using MES.Shared.Models.Rotors;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MES.Server.Data.Repositories
{
    public class IncomingInspectionRepository : IIncomingInspection
    {
        private readonly ProjectdbContext _context;

        public IncomingInspectionRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IncomingInspection>> GetAllAsync()
        {
            return await _context.IncomingInspections.Include(i => i.Images).ToListAsync();
        }

        public async Task<IncomingInspection?> GetByIdAsync(int id)
        {
            return await _context.IncomingInspections
                                 .Include(i => i.Images)
                                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IncomingInspection> AddAsync(IncomingInspection inspection)
        {
            _context.IncomingInspections.Add(inspection);
            await _context.SaveChangesAsync();
            return inspection;
        }

        public async Task<IncomingInspection?> UpdateAsync(int id, IncomingInspection inspection)
        {
            var existing = await _context.IncomingInspections.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(inspection);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.IncomingInspections.FindAsync(id);
            if (existing == null) return false;

            _context.IncomingInspections.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}