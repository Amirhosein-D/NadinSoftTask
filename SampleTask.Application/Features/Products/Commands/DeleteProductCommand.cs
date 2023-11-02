using MediatR;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseCommandResponse>
        {
            private readonly IProductRepository _productRepository;

            public DeleteProductCommandHandler(IProductRepository projectRepository)
            {
                _productRepository = projectRepository;
            }

            public async Task<BaseCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var response = new BaseCommandResponse();

                var project = await _productRepository.GetByIdAsync(request.Id);

                if (project == null)
                {
                    response.Success = false;
                    response.Message = "deleted faild";
                    return response;
                }

                await _productRepository.DeleteAsync(project);

                response.Success = true;
                response.Message = "deleted faild";
                return response;
            }
        }

    }
}
