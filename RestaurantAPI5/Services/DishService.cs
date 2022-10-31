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
        private readonly RestaurantDbContext Context;
        private readonly IMapper Mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }


        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = Context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var dishEntity = Mapper.Map<Dish>(dto);

            Context.Dishes.Add(dishEntity);
            Context.SaveChanges();

            return dishEntity.Id;
        }
    }
}
