using AutoMapper;
using MediatR;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Application.Models.DTOs.Products;
using SampleTask.Application.Models.DTOs.Products.Validators;
using SampleTask.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Features.Products.Commands
{
    public class EditProductCommand : IRequest<BaseCommandResponse>
    {
        public EditProductDto EditProductDto { get; set; }

        public class EditProductCommandHandler : IRequestHandler<EditProductCommand, BaseCommandResponse>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;

            public EditProductCommandHandler(IMapper mapper, IProductRepository productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }

            public async Task<BaseCommandResponse> Handle(EditProductCommand request, CancellationToken cancellationToken)
            {
                var response = new BaseCommandResponse();
                var validator = new EditProductDtoValidator();
                var result = await validator.ValidateAsync(request.EditProductDto);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "edited failld";
                    response.Erorrs = result.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var project = await _productRepository.GetByIdAsync(request.EditProductDto.Id);

                if (project == null)
                {
                    response.Success = false;
                    response.Message = "edited failld";
                    return response;
                }

                _mapper.Map(request.EditProductDto, project);
                await _productRepository.UpdateAsync(project);

                response.Success = true;
                response.Message = "edited successful";
                return response;
            }
        }

    }
}
