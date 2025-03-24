using MES.Shared.Entities;

namespace MES.Server.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
