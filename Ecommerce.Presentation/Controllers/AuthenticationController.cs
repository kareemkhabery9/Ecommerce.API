using Ecommerce.Services.Abstraction;
using Ecommerce.Shared.IdentityDTOs;
using Ecommerce.Shared.IdentityDTOs.Ecommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService=authenticationService;
        }

        //Login
        //Post:baseUrl/api/Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authenticationService.LoginAsync(loginDTO);

            return HandleResult(result);
        }


        //Register
        //Post:baseUrl/api/Authentication/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var result = await _authenticationService.RegisterAsync(registerDTO);

            return HandleResult(result);

        }


        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            var result = await _authenticationService.CheckEmailExistAsync(email);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authenticationService.GetUserByEmailAsync(email!);

            return HandleResult(result);
        }


        [ProducesResponseType<AddressDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpGet("Address")]
        //baseUsr/api/Authentication/Address

        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authenticationService.GetAddressAsync(email!);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPut("Address")]
        //baseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authenticationService.UpdateUserAddressAsync(email!, addressDTO);
            return HandleResult(result);
        }
    }
}
