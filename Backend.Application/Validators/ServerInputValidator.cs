using Backend.Application.Dtos.Input;
using FluentValidation;

namespace Backend.Application.Validators
{
    public class ServerInputValidator : AbstractValidator<ServerDtoInput>
    {
        public ServerInputValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull();
            RuleFor(dto => dto.IpAddress).NotEmpty().NotNull();
            RuleFor(dto => dto.Port).GreaterThan(0);
        }
    }
}