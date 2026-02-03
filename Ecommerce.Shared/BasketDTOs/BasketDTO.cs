using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; set; } = default!;
        ICollection<BasketItemsDTO> Items { get; set; } = [];

    }
}
