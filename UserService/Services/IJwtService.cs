using UserService.Models;

namespace UserService.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserModel user);
    }
}
