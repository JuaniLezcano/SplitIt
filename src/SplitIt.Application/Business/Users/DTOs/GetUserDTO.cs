namespace SplitIt.Application.Business.Users.DTOs
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

    }
}
