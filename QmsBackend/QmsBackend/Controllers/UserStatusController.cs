using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatusController : Controller
    {
        private readonly IUserSuccessService _iUserSuccessService;
        public UserStatusController(IUserSuccessService iUserSuccessService)
        {
            _iUserSuccessService = iUserSuccessService;
        }
        [HttpGet]
        public async Task<IActionResult> getsuccess()
        {
            var obj=await _iUserSuccessService.GetAllAsync();
            if(obj != null)
            {
                return Ok(obj);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> addsuccess([FromBody] UserSuccessDto userSuccessDto)
        {
            if(userSuccessDto != null) { 
               await _iUserSuccessService.AddAsync(userSuccessDto);
                return Ok(userSuccessDto);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> updatesuccess([FromBody] UserSuccessDto userSuccessDto)
        {
            if(userSuccessDto != null)
            {
               await _iUserSuccessService.UpdateAsync(userSuccessDto); return Ok("updated");
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
           await _iUserSuccessService.RemoveAsync(id);
            return Ok("deleted");
        }
    }
}
