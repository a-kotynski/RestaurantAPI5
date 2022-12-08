using Microsoft.EntityFrameworkCore;
using RestaurantAPI5.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI5
{
    public class RestaurantSeeder
    {
        public readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an Amemem blablABLA asdfpuvnq...",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Szczebrzeszyn",
                        Street = "Krótka 13",
                        PostalCode = "69-666"
                    }
                },
                new Restaurant()
                {
                    Name = "Asdf",
                    Category = "Slow Food",
                    Description = "Asdf (short for Asdf Fried Chicken) is an Abcd blablABLA asdfpuvnq...",
                    ContactEmail = "contact@asdf.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Asdf",
                            Price = 10.30M,
                        },
                        new Dish()
                        {
                            Name = "Asdf Nuggets",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Grzahszczrzbółczżyn",
                        Street = "Okropnie Długa 13",
                        PostalCode = "00-420"
                    }
                }
            };
            return restaurants;
        }
    }
}
