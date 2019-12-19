using System.Threading.Tasks;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface ICaptainService
    {
        Task<UserActionResponse> AddAsync(Captain captain);
    }
}