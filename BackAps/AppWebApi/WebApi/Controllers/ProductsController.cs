using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.SignalRhub;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRequestClient<GetAllProductsRequest> _productClient;
        private readonly IRequestClient<AddProductRequest> _client;
        private readonly IHubContext<ProductHub> _hub;

        public ProductsController(IRequestClient<GetAllProductsRequest> productClient, IRequestClient<AddProductRequest> client,
                                  IHubContext<ProductHub> hub)
        {
            _productClient = productClient;
            _client = client;
            _hub = hub;
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productClient.GetResponse<GetAllProductsResponse>(new GetAllProductsRequest());

            return Ok(response.Message.Products);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var response = await _client.GetResponse<AddProductResponse>(request);

            if (!response.Message.Success)
                return BadRequest(response.Message.Message);

            await _hub.Clients.All.SendAsync("ProductAdded", $"Новый товар добавлен: {request.Name} — {request.Price}₽");

            return Ok(new { message = response.Message.Message });
        }
    }
}
