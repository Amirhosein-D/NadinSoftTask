using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Models.DTOs.Products
{
    public class CreateProductDto : IProductDto
    {
        public string Name { get; set; }

        public bool IsAvailable { get; set; }
    }
}
