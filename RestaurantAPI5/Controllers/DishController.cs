using Microsoft.AspNetCore.Mvc;
using RestaurantAPI5.Models;
using RestaurantAPI5.Services;
using System;

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


        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDishId = DishService.Create(restaurantId, dto);

            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }
    }
}
