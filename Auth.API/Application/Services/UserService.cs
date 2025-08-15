using Auth.API.Application.InputModels;
using Auth.API.Application.OrderService.Application.Abstractions;
using Auth.API.Core.Auth;
using Auth.API.Core.Entities;
using Auth.API.Core.Repositories;
using Serilog;

namespace Auth.API.Application.Services
{
    public class UserService(IAuthService authService, IUserRepository userRepository) : IUserService
    {
        public async Task<Result> AddUser(AddUserInputModel input)
        {
            var dbIsHealthy = await userRepository.CheckHealthAsync();

            if (!dbIsHealthy)
            {
                return Result.Failure("Database connection is not healthy");
            }

            if (string.IsNullOrEmpty(input.Username) || string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
            {
                Log.Error("Invalid input data for adding user: {Input}", input);
                return Result.Failure("Invalid input data");
            }

            var passwordHash = authService.ComputeSha256Hash(input.Password);

            var user = new User(input.Username, input.Email, passwordHash);

            await userRepository.AddAsync(user);

            Log.Error("User added successfully: {Username}", input.Username);
            return Result.Success("User added successfully");
        }

        public async Task<Result<string>> Login(LoginInputModel input)
        {
            var dbIsHealthy = await userRepository.CheckHealthAsync();

            if (!dbIsHealthy)
            {
                return Result<string>.Failure("Database connection is not healthy");
            }

            if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
            {
                return Result<string>.Failure("Email and Password are required");
            }


            var passwordHash = authService.ComputeSha256Hash(input.Password);

            var user = await userRepository.GetByEmailAndPasswordAsync(input.Email, passwordHash);

            if (user == null)
            {
                Log.Error("User not found for login: {Input}", input);
                return Result<string>.Failure("User not found");
            }

            var bearerToken = authService.GenerateJwtToken(user.Id, "Admin");

            return Result<string>.Success(bearerToken);
        }

        public async Task<Result<List<User>>> GetAllUsers()
        {
            var dbIsHealthy = await userRepository.CheckHealthAsync();

            if (!dbIsHealthy)
            {
                return Result<List<User>>.Failure("Database connection is not healthy");
            }

            var users = await userRepository.GetAllAsync();

            return Result<List<User>>.Success(users.ToList());
        }
    }
}
