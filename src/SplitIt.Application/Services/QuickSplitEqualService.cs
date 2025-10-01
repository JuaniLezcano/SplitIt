using SplitIt.Application.DTOs;
using SplitIt.Application.Interfaces;

namespace SplitIt.Application.Services
{
    public class QuickSplitEqualService : IQuickSplitEqualService
    {
        public List<QuickDebtDTO> CalculateDebts(QuickExpenseDTO expense)
        {
            var total = expense.Payments.Sum(p => p.Amount);
            var equalShare = total / expense.Participants.Count;

            var paid = expense.Participants.ToDictionary(p => p.ParticipantName, _ => 0m);  // Se inicializa en 0m para que interprete que es un decimal.
            foreach (var payment in expense.Payments)
                paid[payment.ParticipantName] += payment.Amount;

            var balances = paid.ToDictionary(
                kv => kv.Key,
                kv => kv.Value - equalShare
            );

            var debts = new List<QuickDebtDTO>();
            var creditors = new Queue<(string, decimal)>(balances.Where(b => b.Value > 0).Select(b => (b.Key, b.Value)));
            var debtors = new Queue<(string, decimal)>(balances.Where(b => b.Value < 0).Select(b => (b.Key, -b.Value)));


            while (creditors.Count > 0 && debtors.Count > 0)
            {
                var (cName, cAmt) = creditors.Dequeue();
                var (dName, dAmt) = debtors.Dequeue();

                var pay = Math.Min(cAmt, dAmt);
                debts.Add(new QuickDebtDTO(dName, cName, pay));

                if (cAmt > dAmt) creditors.Enqueue((cName, cAmt - pay));
                else if (dAmt > cAmt) debtors.Enqueue((dName, dAmt - pay));
            }

            return debts;
        }
    }
}
