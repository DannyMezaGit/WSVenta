using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.Request;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            return Ok(model);
        }
    }
}
