using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RestaurantAPI5.Entities;
using RestaurantAPI5.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI5.Services;

namespace RestaurantAPI5.Controllers
{
    //mapping requests with specific paths:
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase // ControllerBase allows access to request and answer context
    {


        public RestaurantController(IRestaurantService restaurantService)
        {
            RestaurantService = restaurantService;
        }
        public IRestaurantService RestaurantService { get; }

        // Update
        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromBody] UpdateRestaurantDto dto, [FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = RestaurantService.Update(id, dto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = RestaurantService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }


        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid) // ModelState represents state of attribute validation - [Required] in CreateRestaurantDto.cs
            {
                return BadRequest(ModelState);
            }

            var id = RestaurantService.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }



        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = RestaurantService.GetAll();

            return Ok(restaurantsDtos);
        }



        [HttpGet("{id}")] // it's a good practice to assign an action by Http attributes
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = RestaurantService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
    }
}