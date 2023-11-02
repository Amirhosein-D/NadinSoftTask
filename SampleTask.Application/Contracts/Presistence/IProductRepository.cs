using SampleTask.Application.Contracts.Persistence;
using SampleTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Contracts.Presistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        
    }
}
