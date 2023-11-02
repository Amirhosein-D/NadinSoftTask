using AutoMapper;
using MediatR;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Application.Models.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Features.Products.Queries
{
    public class GetProductDetailRequest : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, ProductDto>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepo;

            public GetProductDetailRequestHandler(IMapper mapper, IProductRepository productRepo)
            {
                _mapper = mapper;
                _productRepo = productRepo;
            }

            public async Task<ProductDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken)
            {
                var result = await _productRepo.GetByIdAsync(request.Id);
                return _mapper.Map<ProductDto>(result);
            }
        }
    }
}
