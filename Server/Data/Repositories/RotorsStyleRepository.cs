using MES.Server.Data.Repositories;
using MES.Server.Data;
using MES.Shared.Models;
using MES.Server.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class RotorsStyleRepository : RepositoryBase<RotorsStyle>, IRotorsStyleRepository
    {

        private readonly ProjectdbContext _loccontext;
        public RotorsStyleRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(RotorsStyle rotors)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(rotors.RotorsStyleName, rotors.Description))
                {

                    _loccontext.rotorsStyles.Add(rotors);
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
            var part = _loccontext.rotorsStyles.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.rotorsStyles.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(RotorsStyle rotors)
        {
            var loc = _loccontext.rotorsStyles.FirstOrDefault(p => p.Id == rotors.Id);

            if (loc != null)
            {
                loc.RotorsStyleName = rotors.RotorsStyleName;
                loc.Description = rotors.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<RotorsStyle>> GetWorkCenterAsync()
        {
            var result = await _loccontext.rotorsStyles.OrderByDescending(RotorsStyle => RotorsStyle).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string workcenters, string description)
        {
            return await _loccontext.rotorsStyles.AnyAsync(x => x.RotorsStyleName == workcenters && x.Description == description);
        }

    }
}












