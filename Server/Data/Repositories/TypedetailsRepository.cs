using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Data.Repositories
{
    public class TypedetailsRepository : RepositoryBase<RotorsStyle>, ITypedetailsRepository
    {

            private readonly ProjectdbContext _loccontext;
            public TypedetailsRepository(ProjectdbContext repositoryContext) : base(repositoryContext)
            {
                _loccontext = repositoryContext;
            }

            public async Task<bool> AddWorkCenterAsync(Typesdetails typesdetails)
            {
                try
                {
                    if (!await CheckIfLocationDescriptionExists(typesdetails.TypeName, typesdetails.Description))
                    {

                        _loccontext.types.Add(typesdetails);
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
                var part = _loccontext.types.FirstOrDefault(p => p.Id == id);

                if (part != null)
                {
                    _loccontext.types.Remove(part);
                    await _loccontext.SaveChangesAsync();
                }
            }

            public async Task<bool> EditWorkCenterAsync(Typesdetails typesdetails)
        {
                var loc = _loccontext.types.FirstOrDefault(p => p.Id == typesdetails.Id);

                if (loc != null)
                {
                    loc.TypeName = typesdetails.TypeName;
                    loc.Description = typesdetails.Description;

                    await _loccontext.SaveChangesAsync();

                    return true;
                }
                return false;
            }

            public async Task<IEnumerable<Typesdetails>> GetWorkCenterAsync()
            {
                var result = await _loccontext.types.OrderByDescending(RotorsStyle => RotorsStyle).ToListAsync();
                return result;
            }


            private async Task<bool> CheckIfLocationDescriptionExists(string workcenters, string description)
            {
                return await _loccontext.rotorsStyles.AnyAsync(x => x.RotorsStyleName == workcenters && x.Description == description);
            }

    }
}

