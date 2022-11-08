using Microsoft.AspNetCore.Identity;
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
        public AccountService(RestaurantDbContext context, IPasswordHasher<User> passwordHasher)
        {
            Context = context;
            PasswordHasher = passwordHasher;
        }

        public RestaurantDbContext Context { get; }
        public IPasswordHasher<User> PasswordHasher { get; }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            var hashedPassword = PasswordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            Context.Users.Add(newUser);
            Context.SaveChanges();
        }
    }
}
