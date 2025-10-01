using SplitIt.Domain.Enums;
namespace SplitIt.Application.DTOs
{
    public class QuickExpenseDTO
    {
        public string? Description { get; set; }
        public ExpenseType Type { get; set; } = ExpenseType.Equal;
        public List<QuickParticipantDTO> Participants { get; set; } = new();
        public List<QuickPaymentDTO> Payments { get; set; } = new();
    }
}
