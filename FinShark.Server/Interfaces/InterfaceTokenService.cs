using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceTokenService
    {
        string CreateToken(AppUser user);

    }
}
