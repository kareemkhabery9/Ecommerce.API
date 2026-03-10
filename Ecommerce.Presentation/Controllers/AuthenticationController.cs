using Ecommerce.Services.Abstraction;
using Ecommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //Post:baseUrl/api/authentication/login
        [HttpPost("Login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authenticationService.LoginAsync(loginDTO);

            return HandleResult(result);
        }


        //Register
        //Post:baseUrl/api/authentication/register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var result = await _authenticationService.RegisterAsync(registerDTO);

            return HandleResult(result);

        }
    }
}
