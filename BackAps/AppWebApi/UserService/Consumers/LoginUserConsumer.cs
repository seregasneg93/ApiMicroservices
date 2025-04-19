using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Data;

namespace UserService.Consumers
{
    public class LoginUserConsumer : IConsumer<LoginUserCommand>
    {
        private readonly UserDbContext _context;

        public LoginUserConsumer(UserDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<LoginUserCommand> context)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                                            u.Login == context.Message.Login &&
                                            u.Password == context.Message.Password); // В будущем — хеш проверка

            if (user is null)
            {
                await context.RespondAsync(new LoginUserResponse(false, "", "Неверный логин или пароль"));
                return;
            }

            await context.RespondAsync(new LoginUserResponse(true, user.Role, "Вход выполнен"));
        }
    }
}
