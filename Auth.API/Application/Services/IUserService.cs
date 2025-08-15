using Auth.API.Application.InputModels;
using Auth.API.Application.OrderService.Application.Abstractions;
using Auth.API.Core.Entities;

namespace Auth.API.Application.Services
{
    public interface IUserService
    {
        Task<Result> AddUser(AddUserInputModel input);

        Task<Result<string>> Login(LoginInputModel input);

        Task<Result<List<User>>> GetAllUsers();
    }
}
