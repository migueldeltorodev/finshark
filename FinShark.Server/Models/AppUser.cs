using Microsoft.AspNetCore.Identity;

namespace FinShark.Server.Models
{
    public class AppUser: IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
