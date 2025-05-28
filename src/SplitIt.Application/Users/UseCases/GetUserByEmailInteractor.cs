using SplitIt.Application.Interfaces;
using SplitIt.Application.Users.DTOs;

namespace SplitIt.Application.Users.UseCases
{
    public class GetUserByEmailInteractor
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDTO> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("El email no puede ser nulo o vacío.", nameof(email));
            }

            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                GetUserDTO dto = new GetUserDTO()
                {
                    Email = existingUser.Email,
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                };
                return dto;
            } else
            {
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");
            }
        }
    }
}
