using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;
using ServiceLayer.ViewModel;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignTasksController : Controller
    {
        private readonly IAssignTasksService _assignTasksService;
        public AssignTasksController(IAssignTasksService assignTasksService)
        {
            _assignTasksService = assignTasksService;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var obj = await _assignTasksService.GetAllAsync();
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(obj);
            }
        }
        [HttpPost]
        public async Task<IActionResult> addassigntask([FromBody] AssignTaskDto assignTaskDto)
        {
            if (assignTaskDto != null)
            {
                await _assignTasksService.AddAsync(assignTaskDto);
                return Ok(assignTaskDto);
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteassigntask(int id)
        {
            await _assignTasksService.RemoveAsync(id);
            return Ok();
        }
        //update state
        [HttpPost("updateState")]
        public async Task<IActionResult> updatestate([FromBody] StatusVm statusVm)
        {
            await _assignTasksService.changeStatusAsync(statusVm);
            return Ok();
        }
    }
}
