using SplitIt.Application.Business.Users.DTOs;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Application.Business.Users.UseCases
{
    public class RegisterUserInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterUserInteractor(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Guid> AddAsync(RegisterUserDTO dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new InvalidOperationException("El email ya está registrado");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
            };

            user.Password = _authService.HashPassword(user, dto.Password);

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}