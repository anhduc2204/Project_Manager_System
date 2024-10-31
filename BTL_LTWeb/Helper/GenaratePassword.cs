using Microsoft.AspNetCore.Identity;

namespace BTL_LTWeb.Models.Helper;

public class GenaratePassword
{
    public static string HashPassword(User user,string password)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.HashPassword(user, password);
    }
}