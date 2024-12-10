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
}
