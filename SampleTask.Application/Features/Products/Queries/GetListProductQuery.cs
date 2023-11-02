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
    public class GetListProductQuery : IRequest<List<ProductDto>>
    {

        public class GetListProductQueryHandlers : IRequestHandler<GetListProductQuery, List<ProductDto>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepo;

            public GetListProductQueryHandlers(IMapper mapper, IProductRepository productRepo)
            {
                _mapper = mapper;
                _productRepo = productRepo;
            }

            public async Task<List<ProductDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                var projects = await _productRepo.GetAllAsync();
                return _mapper.Map<List<ProductDto>>(projects);
            }
        }

    }
}
