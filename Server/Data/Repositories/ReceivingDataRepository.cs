using MES.Server.Contracts;
using MES.Server.Data;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReceivingDataRepository : IReceivingDataRepository
{
    private readonly ProjectdbContext _context;

    public ReceivingDataRepository(ProjectdbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Receiving>> GetAllAsync()
    {
        return await _context.Receivings.ToListAsync();
    }

    public async Task<Receiving> GetByIdAsync(int id)
    {
        return await _context.Receivings.FindAsync(id);
    }

    public async Task<Receiving> CreateAsync(Receiving data)
    {
        // Generate Serial Number
        int count = await _context.Receivings.CountAsync() + 1;
        data.SerialNumber = $"MES{count.ToString("D5")}";

        _context.Receivings.Add(data);
        await _context.SaveChangesAsync();
        return data;
    }

    public async Task<bool> UpdateAsync(Receiving data)
    {
        var existingData = await _context.Receivings.FindAsync(data.Id);
        if (existingData == null)
            return false;

        existingData.SelectedOption = data.SelectedOption; // Update dropdown value
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var data = await _context.Receivings.FindAsync(id);
        if (data == null)
            return false;

        _context.Receivings.Remove(data);
        await _context.SaveChangesAsync();
        return true;
    }
}
