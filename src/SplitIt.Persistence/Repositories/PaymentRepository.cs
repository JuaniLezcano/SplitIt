using Microsoft.EntityFrameworkCore;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<User>, IPaymentRepository
    {
        private readonly SplitItDbContext _context;

        public PaymentRepository(SplitItDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
