using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CrediCardValidation : AbstractValidator<CrediCard>
    {
        public CrediCardValidation()
        {
            RuleFor(p => p.Number.ToString()).Must(StartWithO);
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.Ccv).NotEmpty();
            RuleFor(p => p.FullName).NotEmpty();
            RuleFor(p => p.Number).NotEmpty();
            RuleFor(p => p.ExpirationMonth).NotEmpty();
            RuleFor(p => p.ExpirationMonth).LessThanOrEqualTo((short)12);
            RuleFor(p => p.ExpirationYear).NotEmpty();
            RuleFor(p => p.ExpirationYear).GreaterThanOrEqualTo((short)DateTime.Now.Year);

        }
        private bool StartWithO(string arg)
        {
            return !arg.StartsWith("0");
        }
    }
}
