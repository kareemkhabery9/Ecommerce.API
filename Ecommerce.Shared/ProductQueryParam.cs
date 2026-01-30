using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared
{
    public class ProductQueryParam
    {
        public int? brandId { get; set; }

        public int? typeId { get; set; }

        public string? search { get; set; }


    }
}
