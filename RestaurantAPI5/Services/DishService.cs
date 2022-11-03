using AutoMapper;
using RestaurantAPI5.Entities;
using RestaurantAPI5.Exceptions;
using RestaurantAPI5.Models;
using System.Linq;

namespace RestaurantAPI5.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);

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
    }
}
