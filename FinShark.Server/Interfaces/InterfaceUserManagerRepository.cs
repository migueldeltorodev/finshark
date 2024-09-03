using FinShark.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceUserManagerRepository
    {
        Task<IdentityResult> CreateAsync(AppUser appUser, string password);
        Task<IdentityResult> AddToRoleAsync(AppUser appUser, string user);
        Task<AppUser?> UserExists(string username);
    }
}
