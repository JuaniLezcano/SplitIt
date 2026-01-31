using SplitIt.Application.Interfaces;
using SplitIt.Application.Users.DTOs;

namespace SplitIt.Application.Users.UseCases
{
    public class UpdateUserInteractor
    {
        private readonly IUserRepository     _userRepository;
        private readonly IAuthService _authService;

        public UpdateUserInteractor(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Guid> UpdateAsync(UpdateUserDTO dto)
        {
            // Validación para que el DTO venga con datos para actualizar
            if (dto.Name is null && dto.Password is null)
                throw new ArgumentException("Debe proporcionar al menos un campo para actualizar.");

            bool modified = false;

            var getUser = await _userRepository.GetByIdAsync(dto.Id);
            if (getUser == null)
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");

            if (dto.Name is not null)
            {
                getUser.Name = dto.Name;
                modified = true;
            }

            if (dto.Password is not null)
            {
                getUser.Password = _authService.HashPassword(getUser, dto.Password);
                modified = true;
            }

            if (!modified)
                throw new InvalidOperationException("No se detectaron cambios para actualizar.");

            await _userRepository.UpdateAsync(getUser);
            return getUser.Id;
        }
    }
}
