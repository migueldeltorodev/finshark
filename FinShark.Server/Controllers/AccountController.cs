using FinShark.Server.Dtos.Account;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly InterfaceTokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, InterfaceTokenService interfaceTokenService)
        {
            _userManager = userManager; 
            _tokenService = interfaceTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.EmailAddress,
                };

                var createdUser = await _userManager.CreateAsync(appUser, register.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }    
                        );
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
