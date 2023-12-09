using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleTask.Application.Contracts.Identity;

namespace SampleTask.Api.Controllers
{
    [Authorize]
    [Authorize(Roles = "superadmin")]
    [Route("api/SuperAdmin")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {



        private readonly IPermissionService _permissionService;

        public SuperAdminController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        [HttpPost]
        public IActionResult AssignPermissionToProduct(string userId, int productId)
        {
            _permissionService.AssignPermissionToProduct(userId, productId);
            return Ok("Permission assigned successfully");
        }



    }
}
