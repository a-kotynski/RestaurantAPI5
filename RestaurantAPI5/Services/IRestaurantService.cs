using RestaurantAPI5.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI5.Services
{
    public interface IRestaurantService
    {
        void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
        int Create(CreateRestaurantDto dto, int userId);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id, ClaimsPrincipal user);
    }
}