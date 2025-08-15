using Auth.API.Core.Entities;

namespace Auth.API.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User order);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> CheckHealthAsync();

        Task<User> GetByEmailAndPasswordAsync(string email, string password);
    }
}
