using Auth.API.Core.Entities;
using Auth.API.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace Auth.API.Infrastructure.repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        public UserRepository(IConfiguration configuration)
        {
            var connectionString =  configuration.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("AuthDb");
            _collection = database.GetCollection<User>("Users");
        }

        public async Task AddAsync(User order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return _collection.Find(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }


        public async Task<bool> CheckHealthAsync()
        {
            try
            {
                var result = await _collection.Database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
                Log.Information("MongoDB health check successful: {Result}", result); 
                return result.GetValue("ok").ToDouble() == 1;
            }
            catch(Exception ex)
            {
                Log.Error(ex, "MongoDB health check failed");
                return false;
            }
        }

    }
}
