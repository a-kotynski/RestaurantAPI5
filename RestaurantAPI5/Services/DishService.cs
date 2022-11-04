using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI5.Entities;
using RestaurantAPI5.Exceptions;
using RestaurantAPI5.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI5.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
    }
    public class DishService : IDishService
    {

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        private readonly RestaurantDbContext Context;
        private readonly IMapper Mapper;


        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = Context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var dishEntity = Mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restaurantId;

            Context.Dishes.Add(dishEntity);
            Context.SaveChanges();

            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = Context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");


            var dish = Context.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            var dishDto = Mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = Context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var dishDtos = Mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDtos;
        }
    }
}
