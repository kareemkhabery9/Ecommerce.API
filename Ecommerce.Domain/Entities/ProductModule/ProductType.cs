using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.ProductModule
{
    public class ProductType : BaseClass<int>
    {
        public string Name { get; set; } = default!;

    }
}
