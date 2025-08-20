using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsersAsync();

            if (result == null)
            {
                return NotFound();
            }

            if (result.Count() == 0)
            {
                return Ok(new { message = "No existen usuarios registrados." });
            }

            return Ok(result);
        }

        [HttpPost("get")] // Fixed route conflict by adding a prefix to distinguish routes  
        public async Task<IActionResult> GetUserBy(short type, string user = "")
        {

            if (string.IsNullOrWhiteSpace(user))
            {
                return BadRequest(new { message = "No se pudo buscar el usuario.", error = "El campo user no puede ser nulo." });
            }

            var result = await _userService.GetUserByAsync(type, user);

            if (result == null)
            {
                return NotFound(new {message= "Usuario no encontrado."});
            }

            if (string.IsNullOrEmpty(result.Id))
            {
                return Ok(new { message = "No existen usuarios registrados." });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserPostDto userPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(userPostDto, userPostDto.Password);

            if (!result.Succeeded)
                return BadRequest(new { message = "No se pudo crear el usuario.", errors = result.Errors });

            return Ok(new { message = "Usuario creado exitosamente." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto userToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateUserAsync(userToUpdate);

            if (!result.Succeeded)
                return BadRequest(new { message = "No se pudo actualizar el usuario.", errors = result.Errors });

            return Ok(new { message = "Usuario actualizado exitosamente." });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.DeleteUserAsync(userDto);

            if (!result.Succeeded)
                return BadRequest(new { message = "No se pudo eliminar el usuario.", errors = result.Errors });

            return Ok(new { message = "Usuario eliminado exitosamente." });
        }
    }
}
