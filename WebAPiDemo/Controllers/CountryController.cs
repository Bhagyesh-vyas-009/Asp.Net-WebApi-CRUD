using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPiDemo.Data;
using WebAPiDemo.Models;

namespace WebAPiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly CountryRepository _countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _countryRepository.GetAllCountries();
            return Ok(countries);
        }

        [HttpGet("{CountryID}")]
        public IActionResult SelectCountryByID(int CountryID)
        {
            CountryModel country = _countryRepository.SelectCountryByID(CountryID);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpDelete("{CountryID}")]
        public IActionResult DeleteCountry(int CountryID)
        {
            var isDeleted = _countryRepository.DeleteCountry(CountryID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult CountryInsert([FromBody] CountryModel country)
        {
            var isInserted = _countryRepository.InsertCountry(country);
            if (country == null)
            {
                return BadRequest();
            }
            if (isInserted)
            {
                return Ok(new { Message = "Country Inserted Succesfully" });
            }
            return StatusCode(500, "An error ocurred while inserting the Country");
        }

        [HttpPut("{CountryID}")]
        public IActionResult CountryUpdate(int CountryID, [FromBody] CountryModel country)
        {
            var isUpdated = _countryRepository.UpdateCountry(country);
            if (country == null || CountryID != country.CountryID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "Country Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the Country");
        }

        [HttpGet("countries")]
        public IActionResult GetContries()
        {
            var countries=_countryRepository.GetCountries();
            if (!countries.Any())
            {
                return NotFound("No Countries found");
            }
            return Ok(countries);
        }

        [HttpGet("States/{CountryID}")]
        public  IActionResult GetStatesByCountryID(int CountryID)
        {
            if(CountryID <= 0) 
                return BadRequest("Invalid CountryID");
            var states=_countryRepository.GetStateByContryID(CountryID);
            if (!states.Any())
                return NotFound("No States found for the givben CountryID");
            return Ok(states);
        }
    }
}
