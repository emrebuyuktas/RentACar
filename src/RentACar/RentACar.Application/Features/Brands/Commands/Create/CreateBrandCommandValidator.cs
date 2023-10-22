using FluentValidation;

namespace RentACar.Application.Features.Brands.Commands.Create;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(200);
    }
}