using FluentValidation;
using ToDoApp.Mobile.Core.Models;

namespace ToDoApp.Mobile.Core.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            //ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.User)
                .NotEmpty().WithMessage("El correo o usuario es obligatorio.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}
