using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
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
        private readonly AppDBcontext _context;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager , RoleManager<IdentityRole> role ,
             AppDBcontext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register user 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model )
        {
            if (ModelState.IsValid) 
            { 
                 var user = new AppUser()//create row for user
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

        /// <summary>
        /// LogIn user and get Jwt Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
              var user = _context.Users.Where(x=>x.UserName==model.UserName).ToList();
                if (result.Succeeded)
                {
                    
                    var token = JwtToken(user[0]);
                    TempData["token"] = token;
                    ReadToken(token);
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

        /// <summary>
        /// Logout User 
        /// </summary>
        /// <returns></returns>
        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult>  Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            
        }

        private IEnumerable<Claim> GetUserClaims(AppUser user)
        {
            IEnumerable<Claim> claims = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim("UserName",user.UserName)
                    
                    };
            return claims;
        }
        /// <summary>
        /// create JWt Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string JwtToken(AppUser user)
        {
            var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            //Generate Token for user - JRozario
            var JWToken = new JwtSecurityToken(
                issuer: "http://localhost:45092/",
                audience: "http://localhost:45092/",
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token - JRozario
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(JWToken);
        }

        /// <summary>
        /// Encode Token and read claims
        /// </summary>
        /// <param name="token"></param>
        private void ReadToken(string token)
        {
            var jwtsec = new JwtSecurityTokenHandler();
            var Readtkn = jwtsec.ReadToken(token) as JwtSecurityToken;
            TempData["Name"]= Readtkn.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.InvariantCultureIgnoreCase)).Value.ToString();
            TempData["Email"] = Readtkn.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.InvariantCultureIgnoreCase)).Value.ToString();
            TempData["UserName"] = Readtkn.Claims.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).Value.ToString();
            
        }

    }
}
