using FluentValidation;
using WebAPiDemo.Models;

namespace WebAPiDemo.Validators
{
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            RuleFor(x => x.StateName).NotNull().NotEmpty().Length(1, 10).WithMessage("State Name must not be blank");
            RuleFor(st => st.StateCode).NotEmpty().Length(2, 1000).WithMessage("State Code must contain at least 2 character");
            RuleFor(st => st.StateID).NotEmpty().NotNull().GreaterThan(0).WithMessage("State ID must be a positive number");
            RuleFor(st => st.CountryID).NotEmpty().NotNull().GreaterThan(0).WithMessage("Country ID must be a positive number");
        }
    }
}
