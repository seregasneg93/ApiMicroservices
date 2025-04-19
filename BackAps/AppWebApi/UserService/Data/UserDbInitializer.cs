using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserDbInitializer
    {
        public static async Task InitAsync(UserDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Users.AnyAsync())
            {
                context.Users.AddRange(
                    new User
                    {
                        Login = "admin",
                        Password = "admin123", //В будущем хешировать
                        Role = "Admin"
                    },
                    new User
                    {
                        Login = "user",
                        Password = "user123",
                        Role = "User"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
