using Microsoft.EntityFrameworkCore;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        private readonly SampleTaskDbContext _context;

        public ProductRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<bool> UserProductCheck(string email , int id)
        {
            var product = await GetByIdAsync(id);

            if (product.ManufactureEmail == email)
                return true;

            return false;
        }


        public async Task<IEnumerable<Product>> GetProductsCreatedByUserIdAsync(string email)
        {
            return await _context.Products.Where(p => p.ManufactureEmail == email).ToListAsync();
        }

    }
}
