using MediatR;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Application.Business.Expenses.CreateExpense;
public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IGenericRepository<Expense> _genericRepo;

    public CreateExpenseHandler(IGenericRepository<Expense> genericRepo)
    {
        _genericRepo = genericRepo;
    }

    public async Task<Guid> Handle(CreateExpenseCommand command, CancellationToken ct)
    {
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Description = command.Description,
            GroupId = command.GroupId,
            CreatedByUserGroupId = command.CreatedByUserGroupId,
            Type = command.Type,
            //Payments = command.InitialPayments Solve this later (mayby calling a payment service to create payments and return their ids, then assign those ids to the expense)
        };
        await _genericRepo.AddAsync(expense);
        return expense.Id;
    }
}
