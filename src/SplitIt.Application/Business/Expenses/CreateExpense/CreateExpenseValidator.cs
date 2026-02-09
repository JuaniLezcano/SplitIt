using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitIt.Application.Business.Expenses.CreateExpense;
public class CreateExpenseValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.")
            .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("El tipo de gasto no es válido.");
        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("El ID del grupo es obligatorio.");
        RuleFor(x => x.CreatedByUserId)
            .NotEmpty().WithMessage("El ID del usuario creador es obligatorio.");
    }
}
