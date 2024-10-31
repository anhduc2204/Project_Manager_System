using BTL_LTWeb.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWeb.Models.Service.IRepository;

public interface IAuthRepository
{
    public Task<bool> Login(LoginDto dto);
    public Task<bool> Register(SignUpdto dto);
}