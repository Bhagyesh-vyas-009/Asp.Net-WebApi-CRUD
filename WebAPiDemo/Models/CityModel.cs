using FluentValidation;
using System;

namespace WebAPiDemo.Models
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        public string CityCode { get; set; }

        public int StateID { get; set; }
        public string? StateName { get; set; }

        public int CountryID { get; set; }
        public string? CountryName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
