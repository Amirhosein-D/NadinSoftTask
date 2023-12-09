using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Domain
{
    public class ApplicationUserProduct
    {
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }

    }
}
