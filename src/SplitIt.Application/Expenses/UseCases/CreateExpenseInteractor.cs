using SplitIt.Application.Expenses.DTOs;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitIt.Application.Expenses;
public class CreateExpenseInteractor
{
    private readonly IGenericRepository<Expense> _genericRepo;

    public CreateExpenseInteractor(IGenericRepository<Expense> genericRepo)
    {
        _genericRepo = genericRepo;
    }

    public async Task<Guid> ExecuteAsync(CreateExpenseDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("La descripción del gasto es obligatoria.");
        if (dto.Amount <= 0)
            throw new ArgumentException("El monto del gasto debe ser mayor a cero.");
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
