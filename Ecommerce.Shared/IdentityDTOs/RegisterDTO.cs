using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.IdentityDTOs
{
    public record RegisterDTO(
        string DisplayName,
        string UserName,
        string Email,
        string Password,
        string PhoneNumber
        );

}
