using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;
using SplitIt.Application.Users.DTOs;
namespace SplitIt.Application.Users.UseCases
{
    public class RegisterUserInteractor
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> AddAsync(RegisterUserDTO dto)
        {
            // Validación simple si el usuario ya existe en el sistema
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new InvalidOperationException("El email ya está registrado");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, // Encripta en dominio o aquí según diseño
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
