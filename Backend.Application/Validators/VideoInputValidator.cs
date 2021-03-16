using Backend.Application.Dtos.Input;
using FluentValidation;

namespace Backend.Application.Validators
{
    public class VideoInputValidator : AbstractValidator<VideoDtoInput>
    {
        public VideoInputValidator()
        {
            RuleFor(dto => dto.VideoContent).NotNull();
            RuleFor(dto => dto.Description).NotEmpty().NotNull();
        }
    }
}