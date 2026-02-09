using MediatR;
using SplitIt.Application.DTOs;
using SplitIt.Domain.Entities;
using SplitIt.Domain.Enums;

namespace SplitIt.Application.Business.Expenses.CreateExpense;
public record CreateExpenseCommand(
    string Description,
    ExpenseType Type,
    Guid GroupId,
    Guid CreatedByUserId,
    List<PaymentDTO> InitialPayments
) : IRequest<Guid>;