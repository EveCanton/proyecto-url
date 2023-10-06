using Microsoft.AspNetCore.Mvc;
using proyecto_url.Data.Interfaces;
using proyecto_url.Entities;
using proyecto_url.Models;

namespace proyecto_url.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el usuario
            }

            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el usuario
            }

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] CreateAndUpdateUserDTO dto)
        {
            _userService.Create(dto);
            return Ok(dto); // Devuelve el DTO creado o podrías devolver el usuario creado
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] CreateAndUpdateUserDTO dto)
        {
            var existingUser = _userService.GetUserById(userId);
            if (existingUser == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el usuario
            }

            _userService.Update(dto, userId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var existingUser = _userService.GetUserById(userId);
            if (existingUser == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra el usuario
            }

            _userService.DeleteUser(userId);
            return NoContent(); // Devuelve 204 (sin contenido) para indicar éxito sin datos
        }
    }
}
