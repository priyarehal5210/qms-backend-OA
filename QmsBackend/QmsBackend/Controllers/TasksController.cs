using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dtos;
using ServiceLayer.Services.IServices;

namespace QmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskService _customService;
        public TasksController(ITaskService customService)
        {
            _customService = customService;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var obj=await _customService.GetAllAsync();
            if(obj == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(obj);
            }
        }
        [HttpPost]
        public async Task<IActionResult> addtask([FromBody]TasksDto taskdto)
        {
            if (taskdto != null)
            { 
                await _customService.AddAsync(taskdto);
                return Ok();
            }
            return BadRequest(taskdto);
        }
        [HttpPut]
        public async Task<IActionResult> updatetask(TasksDto postTasks)
        {
            if (postTasks.Id!=0)
            {
                await _customService.UpdateAsync(postTasks);
                return Ok(postTasks);
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deletetask(int id)
        {
            await _customService.RemoveAsync(id);
            return Ok();
        }
    }
}
