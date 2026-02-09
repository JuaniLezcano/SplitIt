using SplitIt.Application.Business.Users.DTOs;
using SplitIt.Application.Interfaces;

namespace SplitIt.Application.Business.Users.UseCases
{
    public class UpdateUserInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UpdateUserInteractor(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Guid> UpdateAsync(Guid userId, UpdateUserDTO dto)
        {
            // Validación para que el DTO venga con datos para actualizar
            if (dto.Name is null && dto.Password is null)
                throw new ArgumentException("Debe proporcionar al menos un campo para actualizar.");

            bool modified = false;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");

            if (dto.Name is not null)
            {
                user.Name = dto.Name;
                modified = true;
            }

            if (dto.Password is not null)
            {
                user.Password = _authService.HashPassword(user, dto.Password);
                modified = true;
            }

            if (!modified)
                throw new InvalidOperationException("No se detectaron cambios para actualizar.");

            await _userRepository.UpdateAsync(user);
            return user.Id;
        }
    }
}
