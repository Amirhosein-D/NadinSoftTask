using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application.Models.DTOs.Products.Validators
{
    public class EditProductDtoValidator : AbstractValidator<EditProductDto>
    {
        public EditProductDtoValidator()
        {
            Include(new IProductDtoValidator());
        }
    }
}
