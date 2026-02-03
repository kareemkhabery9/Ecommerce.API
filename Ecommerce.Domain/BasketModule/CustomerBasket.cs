using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.BasketModule
{

    // this entity wont be persisted in the database so we dont need to add any EF Core attributes, configurations or Migrations
    public class CustomerBasket
    {

        public string Id { get; set; } = default!; // Created From Front-end [GUID]

        public ICollection<BasketItems> Items { get; set; } = [];
    }
}
