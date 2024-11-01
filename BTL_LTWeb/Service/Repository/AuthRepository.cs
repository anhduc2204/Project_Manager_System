


using System.Security.Claims;
using BTL_LTWeb.Models.Common;
using BTL_LTWeb.Models.dto;
using BTL_LTWeb.Models.Service.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Models.Service.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Login(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u=>u.UserRoles)
            .ThenInclude(u=>u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user!= null)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result==PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()!.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                var context = _httpContextAccessor.HttpContext;
                await context!.SignInAsync(principal);
                return true;
            }
        }

        return false;
    }

    public async Task<bool> Register(SignUpdto dto)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Address = string.Empty,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt =DateTime.UtcNow
        };

        user.PasswordHash = Helper.GenaratePassword.HashPassword(user, dto.password);
        var roleId = await _context.Roles.FirstOrDefaultAsync(u => u.Name == RoleName.User);
        if (roleId == null) return false;
        var userRole = new UserRole()
        {
            UserId = user.Id,
            RoleId = roleId.Id
        };
        
        

        await _context.Users.AddAsync(user);
        await _context.UserRoles.AddAsync(userRole);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}