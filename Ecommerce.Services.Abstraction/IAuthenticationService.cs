using Ecommerce.Shared.CommenResponses;
using Ecommerce.Shared.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        //Login
        //Email, Password => Token, DisplayName, Email
        Task<Result<UserDTO>> LoginAsync (LoginDTO loginDTO);


        //Register
        //EMail, Password, DisplayName,UserName ,PhoneNumber => Token, DisplayName, Email
        Task<Result<UserDTO>> RegisterAsync (RegisterDTO registerDTO);

    }
}
