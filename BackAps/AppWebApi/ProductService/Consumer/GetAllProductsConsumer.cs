using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

namespace ProductService.Consumer
{
    public class GetAllProductsConsumer : IConsumer<GetAllProductsRequest>
    {
        private readonly ProductDbContext _context;

        public GetAllProductsConsumer(ProductDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<GetAllProductsRequest> context)
        {
            var products = await _context.Products
                .Select(p => new ProductDto(p.Name, p.Description, p.Price))
                .ToListAsync();

            await context.RespondAsync(new GetAllProductsResponse(products));
        }
    }
}
