using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;
using ServiceLayer.ViewModel;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        public RegisterController(IRegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }
        [HttpGet]
        public async Task<IActionResult> getusers()
        {
            var users=await _registerUserService.GetAllAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> adduser([FromBody]RegisterUserDto registerUserDto)
        {
            await _registerUserService.AddAsync(registerUserDto);
            return Ok(registerUserDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginVm loginVm)
        {
            var user=_registerUserService.Login(loginVm.Email, loginVm.Password);
            return Ok(user);
        }
        //approve api
        [HttpPost("approve")]
        public async Task<IActionResult> approve([FromBody] ApproveVm approveVm)
        {
           await _registerUserService.approveuserAsync(approveVm);
            return Ok();
        }

    }
}
