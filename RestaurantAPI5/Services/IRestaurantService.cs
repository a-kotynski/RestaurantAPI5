using RestaurantAPI5.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI5.Services
{
    public interface IRestaurantService
    {
        void Update(int id, UpdateRestaurantDto dto);
        int Create(CreateRestaurantDto dto);
        PagedResult<RestaurantDto> GetAll(RestaurantQuery query);
        RestaurantDto GetById(int id);
        void Delete(int id);
    }
}