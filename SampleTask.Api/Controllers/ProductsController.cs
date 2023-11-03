using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleTask.Application.Features.Products.Commands;
using SampleTask.Application.Features.Products.Queries;
using SampleTask.Application.Models.DTOs.Products;
using SampleTask.Application.Models.Response;

namespace SampleTask.Api.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            var products = await _mediator.Send(new GetListProductQuery());

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductDetailRequest { Id = id });

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Create(CreateProductDto model)
        {
            var product = new CreateProductCommand { CreateProductDto = model };

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Edit(int id, EditProductDto model)
        {
            var product = new EditProductCommand { EditProductDto = model };

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var product = new DeleteProductCommand { Id = id };

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        

    }
}
