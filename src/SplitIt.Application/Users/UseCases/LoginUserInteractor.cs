using SplitIt.Application.Interfaces;
using SplitIt.Application.Users.DTOs;

namespace SplitIt.Application.Users.UseCases
{
    public class LoginUserInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginUserInteractor(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Guid> LoginAsync(LoginUserDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user is null)
                throw new InvalidOperationException("Email o contraseña inválidos");

            var isValid = _authService.VerifyPassword(user, user.Password, dto.Password);
            if (!isValid)
                throw new InvalidOperationException("Email o contraseña inválidos");

            return user.Id;
        }
    }
}
