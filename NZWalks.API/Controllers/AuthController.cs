using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Collections.Generic;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenGenerator tokenGenerator;

        public AuthController(UserManager<IdentityUser> userManager,ITokenGenerator tokenGenerator)
        {
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
        }
        [HttpPost]
        [Route("register/user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.UserName
            };
            IdentityResult identityResult = await this.userManager.CreateAsync(user, registerUserDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerUserDTO.Roles != null && registerUserDTO.Roles.Any())
                {
                    identityResult = await this.userManager.AddToRolesAsync(user, registerUserDTO.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("User created successfully");
                }
            }
            return BadRequest("Something went wrong");
        }
        [HttpPost]
        [Route("login/user")]
        public async Task<IActionResult> RequestLoginAsync([FromBody] RequestLoginDTO requestLoginDTO)
        {
            IdentityUser? identityUser =  await this.userManager.FindByEmailAsync(requestLoginDTO.UserName);
            if (identityUser != null)
            {
                Boolean isPasswordVerified = await this.userManager.CheckPasswordAsync(identityUser, requestLoginDTO.Password);
                if (isPasswordVerified)
                {
                    IList<String> roles = await this.userManager.GetRolesAsync(identityUser);
                    if (roles.Any()) 
                    {
                        String token = this.tokenGenerator.GenerateToken(identityUser, roles.ToList());
                        return Ok(token);
                    }
                }
            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
