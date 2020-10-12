using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Storage;
using register.Models;
using register.Models.DBcontext;
using register.ViewModels.Account;

namespace register.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
       


        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager , RoleManager<IdentityRole> role)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /////register Action
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model )
        {
            if (ModelState.IsValid) 
            { 
                 var user = new AppUser()
                {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true,
                FirstName=model.FirstName,
                LastName=model.LastName
                 };

               var result =await _userManager.CreateAsync(user,model.Password );
                var user1 = await _userManager.FindByEmailAsync(model.Email);
               
                if (result.Succeeded )
                {
                    var result1 =await _userManager.AddToRoleAsync(user1, "User");
                    if (result1.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                     
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        ////login action
        [HttpPost]
        public async Task<IActionResult>  Login(LoginViewModel model)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
              var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password , false,true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "Your Acoount is Locked !!!!";
                    return View(model);
                }

                ViewData["ErrorMessage"] = "نام کاربری یا رمز عبور اشتباه است";
                return View(model);
            }
            return View();
        }

        ////logout Action
        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult>  Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            

        }


    }
}
