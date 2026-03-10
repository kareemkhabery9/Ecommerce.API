using Ecommerce.Domain.Entities.IdentityModule;
using Ecommerce.Services.Abstraction;
using Ecommerce.Shared.CommenResponses;
using Ecommerce.Shared.IdentityDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return Error.InvalidCredintals("User.InvalidCredintals");

            var IsValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!IsValidPassword)
                return Error.InvalidCredintals("User.InvalidCredintals");

            return new UserDTO(user.Email!, user.DisplayName, "Token");

        }



        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {

            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,
            };

          var IdentityResult = await _userManager.CreateAsync(user, registerDTO.Password);

            if(IdentityResult.Succeeded)
                return new UserDTO(user.Email!, user.DisplayName, "Token");
           

            return IdentityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();


        }

    }
}
