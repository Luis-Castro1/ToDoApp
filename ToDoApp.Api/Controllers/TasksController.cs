using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllAsync();

            if (result == null)
            {
                return NotFound();
            }

            if (result.Count() == 0)
            {
                return Ok(new { message = "No existen tareas registradas." });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTasksDto tasksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskService.CreateAsync(tasksDto);

            if (!result)
            {
                return BadRequest(new { message = "No se pudo crear la tarea."});
            }


            return Ok(new { message = "Tarea creada exitosamente." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDto tasksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskService.UpdateAsync(tasksDto);

            if (!result)
            {
                return BadRequest(new { message = "No se pudo modificar la tarea."});
            }


            return Ok(new { message = "Tarea modificada exitosamente." });
        }

        [HttpPatch]
        public async Task<IActionResult> Check([FromBody] CompleteTaskDto tasksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskService.CompleteTaskAsync(tasksDto);

            if (!result)
            {
                return BadRequest(new { message = "No se pudo completar la tarea." });
            }


            return Ok(new { message = "Tarea completada exitosamente." });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] TaskDto taskToDelete)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _taskService.DeleteAsync(taskToDelete);


            if (!result)
            {
                return BadRequest(new { message = "No se pudo eliminar la tarea." });
            }

            return Ok(new { message = "Tarea eliminada correctamente" });
        }
    }
}
