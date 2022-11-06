using RestaurantAPI5.Entities;
using RestaurantAPI5.Models;

namespace RestaurantAPI5.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        public AccountService(RestaurantDbContext context)
        {
            Context = context;
        }

        public RestaurantDbContext Context { get; }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            Context.Users.Add(newUser);
            Context.SaveChanges();
        }
    }
}
