using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI5.Authorization
{
    public class CreatedMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
        public int MinimumRestaurantsCreated { get; }
    }
}
