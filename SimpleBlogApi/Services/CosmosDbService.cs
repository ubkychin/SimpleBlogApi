using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using SimpleBlogApi.Models;

namespace SimpleBlogApi.Services {
    public class CosmosDbService : ICosmosDbService {
        private Container _container;
        public CosmosDbService (
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName) {
            _container = cosmosDbClient.GetContainer (databaseName, containerName);
        }
        public async Task AddAsync (Post post) {
            var str = System.Text.Json.JsonSerializer.Serialize<Post>(post);
            await _container.CreateItemAsync (post, new PartitionKey (post.Id));
        }
        public async Task DeleteAsync (string id) {
            await _container.DeleteItemAsync<Post> (id, new PartitionKey (id));
        }
        public async Task<Post> GetAsync (string id) {
            try {
                var response = await _container.ReadItemAsync<Post> (id, new PartitionKey (id));
                return response.Resource;
            } catch (CosmosException) //For handling post not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Post>> GetMultipleAsync (string queryString) {
            var query = _container.GetItemQueryIterator<Post> (new QueryDefinition (queryString));
            var results = new List<Post> ();
            while (query.HasMoreResults) {
                var response = await query.ReadNextAsync ();
                results.AddRange (response.ToList());
            }
            return results;
        }

        // public async Task UpdateAsync (string id, Post post) {
        //     await _container.UpsertPostAsync (post, new PartitionKey (id));
        // }
    }
}