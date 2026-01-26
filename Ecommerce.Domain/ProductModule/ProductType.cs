using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.ProductModule
{
    public class ProductType : BaseClass<int>
    {
        public string Name { get; set; } = default!;

    }
}
