using SplitIt.Application.DTOs;

namespace SplitIt.Application.Interfaces
{
    public interface IQuickSplitEqualService
    {
        List<QuickDebtDTO> CalculateDebts(QuickExpenseDTO expense);
    }
}
