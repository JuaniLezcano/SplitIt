using SplitIt.Domain.Entities;
using SplitIt.Domain.Enums;

namespace SplitIt.Application.Expenses.DTOs;
internal class CreateExpenseDTO
{
    public string description { get; set; } = default!;
    public ExpenseType type { get; set; }
    public string createdBy { get; set; } = default!; // Name of the user who created the expense
}
