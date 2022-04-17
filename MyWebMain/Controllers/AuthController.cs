using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;
using MyWebMain.Models;

namespace MyWebMain.Controllers
{
   
    public class AuthController : Controller
    {
        IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("[controller]/Login")]
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {

                return View(login);
            }
            else
            {
                var _client = _httpClientFactory.CreateClient("EshopClient");
                
            var respone= _client.PostAsJsonAsync<LoginViewModel>("api/Authentication", login).Result;

                if (respone.IsSuccessStatusCode)
                {
                    var token = respone.Content.ReadAsAsync<TokenModel>().Result;

                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.NameIdentifier,login.UserName),
                        new Claim(ClaimTypes.Name,login.UserName),
                        new Claim("AccsessToken",token.Token)

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true

                    };
                    HttpContext.SignInAsync(principal, properties);
                    return Redirect("~/Home");
                }
                
                else{
                    ModelState.AddModelError("UserName", "User Not Valid");
                    return View(login);
                }

            }
            

            
        }
    }
}