using RestaurantAPI5.Models;
using System.Collections.Generic;

namespace RestaurantAPI5.Services
{
    public interface IRestaurantService
    {
        bool Update(int id, UpdateRestaurantDto dto);
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Delete(int id);
    }
}