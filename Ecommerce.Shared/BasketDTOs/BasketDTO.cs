using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.BasketDTOs
{
    public record BasketDTO(string Id,ICollection<BasketItemsDTO> Items);

}
