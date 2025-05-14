using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class NewBoxRequiredNumberRepository : RepositoryBase<NewBoxRequiredNumber>, INewBoxRequiredNumberRepository
    {
        private readonly ProjectdbContext _loccontext;
        public NewBoxRequiredNumberRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(NewBoxRequiredNumber rotors)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(rotors.NewBoxRequiredNumberName, rotors.Description))
                {

                    _loccontext.NewBoxRequiredNumbers.Add(rotors);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("New Box Required Number with the same combination of New Box Required Number name and description already exists.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string saddlepartNumbers, string description)
        {
            return await _loccontext.NewBoxRequiredNumbers.AnyAsync(x => x.NewBoxRequiredNumberName == saddlepartNumbers && x.Description == description);
        }

        public async Task DeleteWorkCenter(int id)
        {
            var part = _loccontext.NewBoxRequiredNumbers.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.NewBoxRequiredNumbers.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(NewBoxRequiredNumber rotors)
        {
            var loc = _loccontext.NewBoxRequiredNumbers.FirstOrDefault(p => p.Id == rotors.Id);

            if (loc != null)
            {
                loc.NewBoxRequiredNumberName = rotors.NewBoxRequiredNumberName;
                loc.Description = rotors.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<NewBoxRequiredNumber>> GetWorkCenterAsync()
        {
            var result = await _loccontext.NewBoxRequiredNumbers.OrderByDescending(RequiredNumbers => RequiredNumbers).ToListAsync();
            return result;
        }
    }
}
