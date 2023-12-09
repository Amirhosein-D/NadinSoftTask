using SampleTask.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Domain
{
    public class Product : BaseDomainEntity
    {
        [StringLength(80)]
        public string Name { get; set; }

        public DateTime ProductDate { get; set; }

        [StringLength(15)]
        public string ManufacturePhone { get; set; }

        [StringLength(100)]
        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<ApplicationUserProduct> ApplicationUserProducts { get; set; }

    }
}
