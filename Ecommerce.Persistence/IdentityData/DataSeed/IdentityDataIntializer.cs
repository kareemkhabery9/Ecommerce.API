using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataIntializer : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataIntializer> _logger;

        public IdentityDataIntializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataIntializer> logger)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _logger=logger;
        }

        public async Task IntailizeAsync()
        {
            try
            {
                if(!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if(!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Kareem Khabery",
                        UserName = "KareemKhabery",
                        Email = "Kareemkhbe6@gmail.com",
                        PhoneNumber = "01149260371"
                    };

                    var User02 = new ApplicationUser
                    {
                        DisplayName = "Ahmed Sayed",
                        UserName = "AhmedSayed",
                        Email = "Sayed12@gmail.com",
                        PhoneNumber = "01150260381"
                    };


                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User02, "Admin");
                }

            }

            catch (Exception ex)
            {
                _logger.LogError($"Error while Seeding DataBase, {ex.Message} happened");
            }

        }
    }
}
