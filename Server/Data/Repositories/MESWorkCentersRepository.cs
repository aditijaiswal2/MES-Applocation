using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class MESWorkCentersRepository : RepositoryBase<MESWorkcenters>, IMESWorkCentersRepository
    {
        private readonly ProjectdbContext _loccontext;
        public MESWorkCentersRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(MESWorkcenters workcenters)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(workcenters.Workcenters, workcenters.Description))
                {

                    _loccontext.MESWorkcenters.Add(workcenters);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("WorkCenter with the same combination of workcenter name and description already exists.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task DeleteWorkCenter(int id)
        {
            var part = _loccontext.MESWorkcenters.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.MESWorkcenters.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(MESWorkcenters workcenters)
        {
            var loc = _loccontext.MESWorkcenters.FirstOrDefault(p => p.Id == workcenters.Id);

            if (loc != null)
            {
                loc.Workcenters = workcenters.Workcenters;
                loc.Description = workcenters.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MESWorkcenters>> GetWorkCenterAsync()
        {
            var result = await _loccontext.MESWorkcenters.OrderByDescending(workcenters => workcenters).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string workcenters, string description)
        {
            return await _loccontext.MESWorkcenters.AnyAsync(x => x.Workcenters == workcenters && x.Description == description);
        }
    }
}
