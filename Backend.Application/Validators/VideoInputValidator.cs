using Backend.Application.Dtos.Input;
using FluentValidation;

namespace Backend.Application.Validators
{
    public class VideoInputValidator : AbstractValidator<VideoDtoInput>
    {
        public VideoInputValidator()
        {
            RuleFor(dto => dto.VideoContent).NotEmpty().NotNull();
            RuleFor(dto => dto.Description).NotEmpty().NotNull();
        }
    }
}