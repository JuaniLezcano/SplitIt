using FluentValidation;

namespace SplitIt.Application.Business.Groups.CreateGroup;
public class CreateGroupValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre del grupo es obligatorio.")
            .MaximumLength(100)
            .WithMessage("El nombre del grupo no puede exceder los 100 caracteres.");
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("La descripción del grupo no puede exceder los 500 caracteres.");
        RuleFor(x => x.AdminUserId)
            .NotEmpty()
            .WithMessage("El ID del usuario administrador es obligatorio.");
    }
}
