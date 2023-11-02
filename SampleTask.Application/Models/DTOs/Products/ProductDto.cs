using SampleTask.Application.Models.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Models.DTOs.Products
{
    public class ProductDto : BaseCommon , IProductDto
    {
        public string Name { get; set; }

        public DateTime ProductDate { get; set; }

        public string ManufacturePhone { get; set; }

        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }
    }
}
