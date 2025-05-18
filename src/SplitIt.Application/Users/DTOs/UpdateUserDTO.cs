namespace SplitIt.Application.Users.DTOs
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
