using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;
using SplitIt.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitIt.Application.Business.Expenses.CreateExpense;
public class CreateExpenseHandler
{
    private readonly IGenericRepository<Expense> _genericRepo;

    public CreateExpenseHandler(IGenericRepository<Expense> genericRepo)
    {
        _genericRepo = genericRepo;
    }

    public async Task<Guid> ExecuteAsync(CreateExpenseCommand dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("La descripción del gasto es obligatoria.");
        if (typeof(ExpenseType).IsEnumDefined(dto.Type) == false)
             throw new ArgumentException("El tipo de gasto no es válido.");
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = dto.Description,
            Amount = dto.Amount,
            GroupId = dto.GroupId,
            CreatedAt = DateTime.UtcNow
        };
        await _genericRepo.AddAsync(expense);
        return expense.Id;
    }
}
