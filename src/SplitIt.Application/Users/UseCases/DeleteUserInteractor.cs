using SplitIt.Application.Interfaces;
using SplitIt.Application.Users.DTOs;

namespace SplitIt.Application.Users.UseCases
{
    public class DeleteUserInteractor
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> DeleteAsync(DeleteUserDTO dto)
        {
            var getUser = await _userRepository.GetByIdAsync(dto.Id);
            if (getUser == null)
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");

            await _userRepository.DeleteAsync(dto.Id);
            return dto.Id;
        }
    }
}
