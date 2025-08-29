using FluentValidation.Results;
using ToDoApp.Mobile.Core.Validators;

namespace ToDoApp.Mobile.Core.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            User = string.Empty;
            Password = string.Empty;
        }

        public string User { get; set; }
        public string Password { get; set; }

        public ValidationResult Validate()
        {
            LoginValidator validationRules = new();
            ValidationResult result = validationRules.Validate(this);
            return result;
        }
    }
}
