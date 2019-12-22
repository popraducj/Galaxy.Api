using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Api.Core.Models.Teams;
using Galaxy.Api.Core.Models.UserModels;

namespace Galaxy.Api.Core.Interfaces
{
    public interface ICrudService<T>
    {
        Task<ActionResponse> AddAsync(T model);
        Task<ActionResponse> UpdateAsync(Dictionary<string, object> model);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}