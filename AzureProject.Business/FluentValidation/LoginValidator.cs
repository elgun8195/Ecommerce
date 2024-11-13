using AzureProject.Entity.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureProject.Business.FluentValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().MinimumLength(4).MaximumLength(20);
            RuleFor(x => x.Password).NotNull().MinimumLength(8).MaximumLength(20);
        }
    }
}
