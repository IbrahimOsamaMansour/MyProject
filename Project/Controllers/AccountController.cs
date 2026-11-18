using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModel;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;

        public AccountController(UserManager<Employee> userManager, SignInManager<Employee> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Registration(RegisterUserViewModel newUser)
        {
             
            if (ModelState.IsValid)
            {
                Employee usermodel = new Employee()
                {
                    UserName = newUser.Username,
                    PasswordHash = newUser.password
                };
               IdentityResult result =  await userManager.CreateAsync(usermodel ,newUser.password);
              
                if(result.Succeeded == true)
                {
                    //creat cookie
                  await  signInManager.SignInAsync(usermodel, false);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {

            }
            return View();
        }

        //=========================================

        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        //=========================================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel log)
        {
            if (ModelState.IsValid)
            {
                //check username password
             Employee usermodel =    await userManager.FindByNameAsync(log.username);
                if(usermodel != null)
                {
                   bool found =  await userManager.CheckPasswordAsync(usermodel,log.password);
                    if (found)
                    {
                        await signInManager.SignInAsync(usermodel, log.rememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName and Password invalied");
                }
            }
            return View();
        }
    }
}
