using FinShark.Server.Dtos.Account;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly InterfaceTokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly InterfaceUserManagerRepository _userManagerRepo;
        public AccountController(InterfaceTokenService interfaceTokenService, SignInManager<AppUser> signInManager, InterfaceUserManagerRepository userManagerRepo)
        {
            _tokenService = interfaceTokenService;
            _signInManager = signInManager;
            _userManagerRepo = userManagerRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var user = await _userManagerRepo.UserExists(loginDto.Username);

                if (user == null)
                    return Unauthorized("Invalid Username!");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded)
                    return Unauthorized("Username not found and/or password incorrect");

                return Ok(
                    new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
                
                var createdUser = await _userManagerRepo.CreateAsync(appUser, register.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManagerRepo.AddToRoleAsync(appUser, "User");

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
