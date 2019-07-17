using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Models;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GraniteHouse.Areas.Identity.Pages.Account
{
    public class AddAdminUserModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AddAdminUserModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            // Create Roles for our Website and Create Super Admin User
            if (! await _roleManager.RoleExistsAsync(SD.AdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
            }
            if (!await _roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));

                var userAdmin = new ApplicationUser
                {
                    UserName = "cesaconsulting10@gmail.com",
                    Email = "cesaconsulting10@gmail.com",
                    PhoneNumber = "6469321992",
                    Name = "Cesa Consulting"
                };

                var resultUser = await _userManager.CreateAsync(userAdmin, "Cesa?2018$");
                await _userManager.AddToRoleAsync(userAdmin, SD.SuperAdminEndUser);
            }

            return Page();
        }
    }
}