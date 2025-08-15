namespace Auth.API.Core.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(Guid id, string role);

        string ComputeSha256Hash(string password);
    }
}
