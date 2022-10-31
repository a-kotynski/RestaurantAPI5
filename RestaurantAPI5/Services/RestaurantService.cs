using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI5.Entities;
using RestaurantAPI5.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using RestaurantAPI5.Exceptions;

namespace RestaurantAPI5.Services
{
    // this service contains logic for:
    // * downloading all restaurants from a database,
    // * downloading specific one based on an id,
    // * creating a new one based an object RestaurantDto
    public class RestaurantService : IRestaurantService
    {
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            DbContext = dbContext;
            Mapper = mapper;
            _logger = logger;
        }


        public RestaurantDbContext DbContext;
        public IMapper Mapper;
        public ILogger _logger;


        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = DbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = DbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            DbContext.Restaurants.Remove(restaurant);
            DbContext.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = DbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var result = Mapper.Map<RestaurantDto>(restaurant);
            return result;
        }


        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = DbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = Mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }


        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = Mapper.Map<Restaurant>(dto);
            DbContext.Restaurants.Add(restaurant);
            DbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
