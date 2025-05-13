using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class MESDelayReasonRepository : RepositoryBase<MESDelayReason>, IMESDelayReasonRepository
    {
        private readonly ProjectdbContext _loccontext;
        public MESDelayReasonRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddDelayReasonAsync(MESDelayReason delayReason)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(delayReason.DelayReason, delayReason.Description))
                {

                    _loccontext.MESDelayReason.Add(delayReason);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("DelayReason with the same combination of DelayReason name and description already exists.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task DeleteDelayReason(int id)
        {
            var part = _loccontext.MESDelayReason.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.MESDelayReason.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditDelayReasonAsync(MESDelayReason delayReason)
        {
            var loc = _loccontext.MESDelayReason.FirstOrDefault(p => p.Id == delayReason.Id);

            if (loc != null)
            {
                loc.DelayReason = delayReason.DelayReason;
                loc.Description = delayReason.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MESDelayReason>> GetDelayReasonAsync()
        {
            var result = await _loccontext.MESDelayReason.OrderByDescending(delayReason => delayReason).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string delayReason, string description)
        {
            return await _loccontext.MESDelayReason.AnyAsync(x => x.DelayReason == delayReason && x.Description == description);
        }
    }
}
