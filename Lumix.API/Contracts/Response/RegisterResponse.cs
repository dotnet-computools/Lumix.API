namespace Lumix.API.Contracts.Response;

public class RegisterResponse
{ 
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
}