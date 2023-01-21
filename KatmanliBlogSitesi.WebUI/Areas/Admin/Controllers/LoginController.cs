using KatmanliBlogSitesi.Entities;
using KatmanliBlogSitesi.Service.Abstract;
using KatmanliBlogSitesi.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace KatmanliBlogSitesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User> _service;

        public LoginController(IService<User> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Admin/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin/Login");
        }


        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    var kullanici = await _service.FirstOrDefaultAsync(k => k.Email == loginViewModel.Email && k.Password == loginViewModel.Password && k.IsActive);
                    if (kullanici != null)
                    {
                        var haklar = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,kullanici.Name),
                        new Claim("Role",kullanici.IsAdmin ? "Admin" : "User"),
                        new Claim("UserId",kullanici.Id.ToString())
                    };
                        var kullaniciKimligi = new ClaimsIdentity(haklar, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new(kullaniciKimligi);
                        await HttpContext.SignInAsync(principal);
                        return Redirect("/Admin/");
                    }
                    else TempData["Mesaj"] = "Giriş Başarısız!";
                }
                catch (Exception)
                {
                    TempData["Mesaj"] = "Hata Oluştu!";

                }
            }
            return View(loginViewModel);
        }

    }
}
