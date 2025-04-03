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

        public async Task AddAsync(IncomingInspection order)
        {
            await _context.IncomingInspections.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.IncomingInspections.FindAsync(id);
            if (order != null)
            {
                _context.IncomingInspections.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncomingInspection>> GetAllAsync()
        {
            return await _context.IncomingInspections.ToListAsync();
        }

        public async Task<IncomingInspection> GetByIdAsync(int id)
        {
            return await _context.IncomingInspections.FindAsync(id);
        }

        public async Task UpdateAsync(IncomingInspection order)
        {
            _context.IncomingInspections.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
