using SplitIt.Application.Interfaces;

namespace SplitIt.Application.Business.Users.UseCases
{
    public class DeleteUserInteractor
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> DeleteAsync(Guid userId)
        {
            var getUser = await _userRepository.GetByIdAsync(userId);
            if (getUser == null)
                throw new InvalidOperationException("El usuario no se encuentra en el sistema");

            await _userRepository.DeleteAsync(userId);
            return userId;
        }
    }
}
