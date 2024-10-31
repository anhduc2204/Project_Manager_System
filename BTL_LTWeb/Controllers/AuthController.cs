using BTL_LTWeb.Models.dto;
using BTL_LTWeb.Models.Service.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Task = BTL_LTWeb.Models.Task;

namespace BTL_LTWeb.Controllers;

public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm]LoginDto dto)
    {
        var loginSuccess = await _authRepository.Login(dto);
        if (loginSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.ErrorMessage="Thông tin đăng nhập không đúng";
        return View();
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]SignUpdto signUpdto)
    {
        var registerSuccess = await _authRepository.Register(signUpdto);
        if (registerSuccess)
        {
            return RedirectToAction("Login", "Auth");
        }

        ViewBag.ErrorMessage="Đăng ký không thành công";
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}