using MES.Shared.DTOs;
using MES.Shared.Models;

namespace MES.Server.Contracts
{
    public interface ILoginUserDetails
    {
        Task<LoginUserDetailsDTO> CreateLoginUserDetailsAsync(LoginUserDetailsDTO loginUserDetailsDto);
        Task <IEnumerable<LoginUserDetailsDTO>> GetAllLoginUserDetailsAsync();
    }
}
