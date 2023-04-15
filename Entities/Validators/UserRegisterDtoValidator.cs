using FluentValidation;
using KanS.Entities;

namespace KanS.Models.Validators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto> {
    public UserRegisterDtoValidator(KansDbContext dbContext) {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Email)
            .Custom((value, context) => {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if(emailInUse) {
                    context.AddFailure("Email", "This email is taken");
                }
            });

        RuleFor(x => x.Password)
            .MinimumLength(5);

        RuleFor(x => x.ConfirmPassword)
            .Equal(e => e.Password);

    }
}