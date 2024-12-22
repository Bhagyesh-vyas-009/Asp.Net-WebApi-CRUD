using FluentValidation;
using WebAPiDemo.Models;

namespace WebAPiDemo.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        public CityValidator()
        {
            RuleFor(ci => ci.CityID).NotEmpty().NotNull().GreaterThan(0).WithMessage("City ID must be a positive number");
            RuleFor(ci => ci.CityName).NotNull().NotEmpty().Length(1, 10).WithMessage("City Name must not be blank");
            RuleFor(ci => ci.CityCode).NotEmpty().Length(2, 1000).WithMessage("City Code must contain at least 2 character");
            RuleFor(ci => ci.StateID).NotEmpty().NotNull().GreaterThan(0).WithMessage("State ID must be a positive number");
            RuleFor(ci => ci.CountryID).NotEmpty().NotNull().GreaterThan(0).WithMessage("Country ID must be a positive number");
        }
    }
}
