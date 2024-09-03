using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Server.Repository
{
    public class UserManagerRepository : InterfaceUserManagerRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserManagerRepository(UserManager<AppUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string user)
        {
            return await _userManager.AddToRoleAsync(appUser, user);
        }

        public async Task<IdentityResult> CreateAsync(AppUser appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<AppUser?> UserExists(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }
    }
}
