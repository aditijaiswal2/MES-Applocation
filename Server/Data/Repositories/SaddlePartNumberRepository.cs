using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class SaddlePartNumberRepository : RepositoryBase<SaddlepartNumber>, ISaddlePartNumberRepository
    {
        private readonly ProjectdbContext _loccontext;
        public SaddlePartNumberRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
        {
            _loccontext = repositoryContext;
        }

        public async Task<bool> AddWorkCenterAsync(SaddlepartNumber rotors)
        {
            try
            {
                if (!await CheckIfLocationDescriptionExists(rotors.SaddlePartNumberName, rotors.Description))
                {

                    _loccontext.saddlepartNumbers.Add(rotors);
                    await _loccontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception("Saddle part Number with the same combination of Saddle Part Number name and description already exists.");
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
            var part = _loccontext.saddlepartNumbers.FirstOrDefault(p => p.Id == id);

            if (part != null)
            {
                _loccontext.saddlepartNumbers.Remove(part);
                await _loccontext.SaveChangesAsync();
            }
        }

        public async Task<bool> EditWorkCenterAsync(SaddlepartNumber rotors)
        {
            var loc = _loccontext.saddlepartNumbers.FirstOrDefault(p => p.Id == rotors.Id);

            if (loc != null)
            {
                loc.SaddlePartNumberName = rotors.SaddlePartNumberName;
                loc.Description = rotors.Description;

                await _loccontext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<SaddlepartNumber>> GetWorkCenterAsync()
        {
            var result = await _loccontext.saddlepartNumbers.OrderByDescending(SaddlePart => SaddlePart).ToListAsync();
            return result;
        }

        private async Task<bool> CheckIfLocationDescriptionExists(string saddlepartNumbers, string description)
        {
            return await _loccontext.saddlepartNumbers.AnyAsync(x => x.SaddlePartNumberName == saddlepartNumbers && x.Description == description);
        }

    }
}

