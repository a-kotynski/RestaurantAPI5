using Microsoft.AspNetCore.Mvc;
using RestaurantAPI5.Models;
using RestaurantAPI5.Services;
using System;
using System.Collections.Generic;

namespace RestaurantAPI5.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        public DishController(IDishService dishService)
        {
            DishService = dishService;
        }
        private readonly IDishService DishService;

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            DishService.RemoveAll(restaurantId);

            return NoContent();
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDishId = DishService.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto dish = DishService.GetById(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            var result = DishService.GetAll(restaurantId);
            return Ok(result);
        }
    }
}
