using SplitIt.Domain.Entities;

namespace SplitIt.Application.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment?> GetByTransactionIdAsync(string transactionId);
    }
}
