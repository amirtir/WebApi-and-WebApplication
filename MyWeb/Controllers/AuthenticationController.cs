using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWeb.Models;

namespace MyWeb.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        [HttpPost]
        public IActionResult Login([FromBody] Login login )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is Not Valid");
            }
            if (login.UserName.ToLower() == "amir" && login.Password.ToLower() == "1234")
            {
                var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ValidationKeyApiVerify"));
                var siningCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);


                var tokenOption = new JwtSecurityToken(

               issuer: "https://localhost:44305",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,login.UserName ),
                    new Claim(ClaimTypes.Role,"Admin")
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: siningCredentials

                    );

                var FinalToken = new JwtSecurityTokenHandler().WriteToken(tokenOption);
                return Ok(new { token = FinalToken });
            }
            else
                return Unauthorized();


        }

        private object JwtSecurityTokenHandler()
        {
            throw new NotImplementedException();
        }
    }
}