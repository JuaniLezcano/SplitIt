namespace SplitIt.Application.Business.Users.DTOs
{
    public class RegisterUserDTO
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
