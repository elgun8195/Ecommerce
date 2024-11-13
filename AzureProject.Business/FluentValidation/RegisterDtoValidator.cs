using AzureProject.Entity.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Business.FluentValidation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().MinimumLength(11).MaximumLength(100);
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password).MinimumLength(4).MaximumLength(100).NotEmpty();
            RuleFor(x => x.ConfirmPassword).MinimumLength(4).MaximumLength(100).NotEmpty();


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                    context.AddFailure("ConfirmPassword", "Password ConfirmPAssword-e beraber olmalidir!");
            });


        }
    }
}
