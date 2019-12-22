using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface ICrudGrpcService<T>
    {
        Task<ActionResponse> AddAsync(T captain);
        Task<ActionResponse> UpdateAsync(T captain);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}