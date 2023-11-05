using AutoMapper;
using MediatR;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Application.Models.DTOs.Products;
using SampleTask.Application.Models.DTOs.Products.Validators;
using SampleTask.Application.Models.Response;
using SampleTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<BaseCommandResponse>
    {
        public CreateProductDto CreateProductDto { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public class CreateProductDtoHandler : IRequestHandler<CreateProductCommand, BaseCommandResponse>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;

            public CreateProductDtoHandler(IMapper mapper, IProductRepository productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }

            public async Task<BaseCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var response = new BaseCommandResponse();
                var validator = new CreateProductDtoValidator();
                var result = await validator.ValidateAsync(request.CreateProductDto);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "creating failld , The form is not filled correctly";
                    response.Erorrs = result.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                var product = _mapper.Map<Product>(request.CreateProductDto);

                product.ManufacturePhone = request.Phone;
                product.ManufactureEmail = request.Email;
                product.ProductDate = DateTime.Now;

                product = await _productRepository.AddAsync(product);
                response.Success = true;
                response.Message = "creating Successful";
                response.Id = product.Id;
                return response;
            }
        }

    }
}
