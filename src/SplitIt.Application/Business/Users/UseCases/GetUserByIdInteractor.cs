using SplitIt.Application.Business.Users.DTOs;
using SplitIt.Application.Interfaces;

namespace SplitIt.Application.Business.Users.UseCases
{
    public class GetUserByIdInteractor
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDTO> GetByIdAsync(Guid userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser != null)
            {
                GetUserDTO dto = new GetUserDTO()
                {
                    Id = existingUser.Id,
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                };
                return dto;
            }
            else
            {
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");
            }
        }
    }
}
