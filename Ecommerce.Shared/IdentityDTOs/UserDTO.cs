using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.IdentityDTOs
{
     public record UserDTO(
         string DisplayName,
        string Email,
        string Token
        );

}
