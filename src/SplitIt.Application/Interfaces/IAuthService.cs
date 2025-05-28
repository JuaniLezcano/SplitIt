using SplitIt.Domain.Entities;

namespace SplitIt.Application.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string providedPassword);
    }
}
