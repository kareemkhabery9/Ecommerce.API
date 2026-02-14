using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class BaseClass<TKey>
    {
        public TKey Id { get; set; } = default!;

    }
}
