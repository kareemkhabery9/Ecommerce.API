using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.IdentityDTOs
{
    namespace Ecommerce.Shared.IdentityDTOs
    {
        public record AddressDTO(
            string FirstName,
            string LastName,
            string City,
            string Street,
            string Country
        );
    }
}
