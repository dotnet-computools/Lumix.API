namespace Lumix.Core.Interfaces.Services;

public interface IUserService
{
    Task<(string AccessToken, string RefreshToken)> Login(string email, string password);
    Task Register(string userName, string email, string password);
    Task<string> RefreshToken(string token);
}