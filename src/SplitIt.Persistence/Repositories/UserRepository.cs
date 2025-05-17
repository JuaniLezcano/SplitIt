using Microsoft.EntityFrameworkCore;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SplitItDbContext _context;

        public UserRepository(SplitItDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
