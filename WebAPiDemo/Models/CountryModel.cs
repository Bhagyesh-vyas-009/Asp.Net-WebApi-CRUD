using FluentValidation;
using System.Text.Json.Serialization;

namespace WebAPiDemo.Models
{
    public class CountryModel
    {
        //[JsonIgnore]
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public string CountryCode { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class CountryDropDownModel
    {
        //[JsonIgnore]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

    public class CountryValidator : AbstractValidator<CountryModel>
    {
        public CountryValidator()
        {
            RuleFor(c=>c.CountryName).NotEmpty().NotNull().MaximumLength(3).WithMessage("Country Name must be contain 3 character");
            RuleFor(c => c.CountryCode).NotEmpty().NotNull().Length(3).WithMessage("Country Code must be contain 3 character");
        }
    }
}
