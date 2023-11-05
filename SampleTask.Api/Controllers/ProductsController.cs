using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleTask.Application.Constants;
using SampleTask.Application.Features.Products.Commands;
using SampleTask.Application.Features.Products.Queries;
using SampleTask.Application.Models.DTOs.Products;
using SampleTask.Application.Models.Response;
using System.Security.Claims;

namespace SampleTask.Api.Controllers
{
    [Authorize]
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IMediator mediator , IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(string? email)
        {

            var products = await _mediator.Send(new GetListProductQuery { Email = email });

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
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var phone = _httpContextAccessor.HttpContext.User.FindFirst(CustomClainType.Uphone)?.Value;

            var product = new CreateProductCommand { CreateProductDto = model , Email = email , Phone = phone};

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Edit(int id, EditProductDto model)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var product = new EditProductCommand { EditProductDto = model , Email = email };

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var product = new DeleteProductCommand { Id = id , Email = email };

            var apiResponse = await _mediator.Send(product);

            return Ok(apiResponse);
        }

        

    }
}
