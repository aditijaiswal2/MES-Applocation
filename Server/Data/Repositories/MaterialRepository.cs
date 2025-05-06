using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class MaterialRepository : RepositoryBase<Materials>, IMaterialRepository
    {
        private readonly ProjectdbContext _loccontext;
        public MaterialRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(Materials rotors)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(rotors.MaterialName, rotors.Description))
                {

                    _loccontext.Material.Add(rotors);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Material with the same combination of Material name and description already exists.");
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
            var part = _loccontext.Material.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.Material.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(Materials rotors)
        {
            var loc = _loccontext.Material.FirstOrDefault(p => p.Id == rotors.Id);

            if (loc != null)
            {
                loc.MaterialName = rotors.MaterialName;
                loc.Description = rotors.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Materials>> GetWorkCenterAsync()
        {
            var result = await _loccontext.Material.OrderByDescending(Materials => Materials).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string material, string description)
        {
            return await _loccontext.Material.AnyAsync(x => x.MaterialName == material && x.Description == description);
        }

    }
}

