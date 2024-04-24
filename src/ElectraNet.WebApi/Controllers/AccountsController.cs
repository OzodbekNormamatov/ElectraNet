using ElectraNet.Service.Services.Users;
using ElectraNet.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectraNet.WebApi.Controllers
{
    public class AccountsController(IUserService userService) : BaseController
    {
        [AllowAnonymous]
        [HttpGet("login")]
        public async ValueTask<IActionResult> LoginAsync(string phone, string password)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await userService.LoginAsync(phone, password)
            });
        }

        [HttpGet("send-code")]
        public async ValueTask<IActionResult> SendCodeAsync(string phone)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await userService.SendCodeAsync(phone)
            });
        }

        [HttpGet("confirm-code")]
        public async ValueTask<IActionResult> ConfirmAsync(string phone, string code)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await userService.ConfirmCodeAsync(phone, code)
            });
        }

        [HttpPatch("reset-password")]
        public async ValueTask<IActionResult> ResetPasswordAsync(string phone, string newPassword)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await userService.ResetPasswordAsync(phone, newPassword)
            });
        }

        [HttpPatch("change-password")]
        public async ValueTask<IActionResult> ChangePasswordAsync(string phone, string oldPassword, string newPassword)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = await userService.ChangePasswordAsync(phone, oldPassword, newPassword)
            });
        }
    }
}
