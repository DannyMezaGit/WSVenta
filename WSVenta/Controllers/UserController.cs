using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta res = new Respuesta();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                res.Exito = 0;
                res.Mensaje = "Usuario o contraseña incorrecto";
                return BadRequest(res);
            }

            res.Exito = 1;
            res.Mensaje = "Login exitoso";
            res.Data = userResponse;
            return Ok(res);
        }
    }
}
