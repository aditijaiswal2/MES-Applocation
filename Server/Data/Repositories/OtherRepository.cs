using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class OtherRepository : RepositoryBase<Other>, IOtherRepository
    {
        private readonly ProjectdbContext _loccontext;
        public OtherRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(Other rotors)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(rotors.OtherName, rotors.Description))
                {

                    _loccontext.Others.Add(rotors);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Other with the same combination of Other name and description already exists.");
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
            var part = _loccontext.Others.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.Others.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(Other rotors)
        {
            var loc = _loccontext.Others.FirstOrDefault(p => p.Id == rotors.Id);

            if (loc != null)
            {
                loc.OtherName = rotors.OtherName;
                loc.Description = rotors.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Other>> GetWorkCenterAsync()
        {
            var result = await _loccontext.Others.OrderByDescending(Other => Other).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string other, string description)
        {
            return await _loccontext.Others.AnyAsync(x => x.OtherName == other && x.Description == description);
        }

    }
}

