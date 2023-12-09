using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Contracts.Identity
{
    public interface IPermissionService
    {

        Task<bool> AssignPermissionToProduct(string userId, int productId);

        Task<bool> UserProductCheckHasRelationshipAsync(string email, int id);

    }
}
