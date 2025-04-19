using Contracts;
using MassTransit;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Consumer
{
    public class AddProductConsumer : IConsumer<AddProductRequest>
    {
        private readonly ProductDbContext _db;

        public AddProductConsumer(ProductDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<AddProductRequest> context)
        {
            var request = context.Message;

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            await context.RespondAsync(new AddProductResponse(true, "Товар добавлен"));
        }
    }
}
