using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleTask.Application.Contracts.Identity;
using SampleTask.Application.Models.Identity;
using SampleTask.Domain;
using SampleTask.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Identity.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly SampleTaskDbContext _context;

        public PermissionService(SampleTaskDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignPermissionToProduct(string userId, int productId)
        {

            var userProduct = new ApplicationUserProduct
            {
                ApplicationUserId = userId,
                ProductId = productId,
            };

            //var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            _context.ApplicationUserProducts.Add(userProduct);
            _context.SaveChanges();

            //if (_context.ApplicationUserProducts.Any(up => up.ApplicationUserId == user.Id && up.ProductId == productId))
            //    return true;

            return true;
        }




        public async Task<bool> UserProductCheckHasRelationshipAsync(string email, int id)
        {
            var product = await _context.Products.FindAsync(id);

            //if (product != null)
            //    if (product != null)
            //    {
            //        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            //        if (user != null)
            //        {
            //            var hasRelation = _context.ApplicationUserProducts.Any(up => up.ApplicationUserId == user.Id && up.ProductId == id);
            //            return hasRelation;
            //        }
            //    }


            if(product == null)
                return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            var result = await _context.ApplicationUserProducts.AnyAsync(up => up.ApplicationUserId == user.Id && up.ProductId == id);
            
            return result;
        }

    }
}
