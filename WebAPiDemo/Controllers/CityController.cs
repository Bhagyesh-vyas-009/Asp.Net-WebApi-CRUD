using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPiDemo.Data;
using WebAPiDemo.Models;

namespace WebAPiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("cities/{StateFilterID}")]
        public IActionResult GetAllCities(int StateFilterID)
        {
            var cities = _cityRepository.GetAllCities(StateFilterID);
            return Ok(cities);
        }

        [HttpGet("{CityID}")]
        public IActionResult GetCityByID(int CityID)
        {
            var city = _cityRepository.SelectCityByPK(CityID);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpDelete("{CityID}")]
        public IActionResult CityDelete(int CityID)
        {
            var isDeleted = _cityRepository.DeleteCity(CityID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpPost]
        public IActionResult CityInsert([FromBody] CityModel city)
        {
            var isInserted = _cityRepository.InsertCity(city);
            if (city == null)
            {
                return BadRequest();
            }
            if (isInserted)
            {
                return Ok(new { Message = "City Inserted Succesfully" });
            }
            return StatusCode(500, "An error ocurred while inserting the city");
        }

        [HttpPut("{CityID}")]
        public IActionResult CityUpdate(int CityID,[FromBody] CityModel city)
        {
            var isUpdated = _cityRepository.UpdateCity(city);
            if (city == null || CityID!=city.CityID)
            {
                return BadRequest();
            }
            if (isUpdated)
            {
                return Ok(new { Message = "City Updated Succesfully" });
            }
            return StatusCode(500, "An error ocurred while updating the city");
        }
    }
}
