using MediatR;
using SampleTask.Application.Contracts.Identity;
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
        public string Email { get; set; }



        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseCommandResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IPermissionService _permissionService;

            public DeleteProductCommandHandler(IProductRepository productRepository, IPermissionService permissionService)
            {
                _productRepository = productRepository;
                _permissionService = permissionService;
            }

            public async Task<BaseCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var response = new BaseCommandResponse();

                if (!await _productRepository.UserProductCheck(request.Email, request.Id)
                    && !await _permissionService.UserProductCheckHasRelationshipAsync(request.Email, request.Id))
                {
                    response.Success = false;
                    response.Message = "You do not have access to delete this product";
                    return response;
                }

                var product = await _productRepository.GetByIdAsync(request.Id);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = "deleted faild , There is no product with this ID";
                    return response;
                }



                await _productRepository.DeleteAsync(product);

                response.Success = true;
                response.Message = "deleted successful";
                return response;
            }
        }

    }
}
