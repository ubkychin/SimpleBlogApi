using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleBlogApi.Models;

namespace SimpleBlogApi.Services {
    public interface ICosmosDbService {
        Task<IEnumerable<Post>> GetMultipleAsync (string query);
        Task<Post> GetAsync (string id);
        Task AddAsync (Post post);
        // Task UpdateAsync (int id, Post post);
        // Task DeleteAsync (int id);
    }
}