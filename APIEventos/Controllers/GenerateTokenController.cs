using APIEventos.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIEventos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateTokenController : ControllerBase
    {
        public ITokenService _tokenService;

        public GenerateTokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult CreateToken(string role)
        {
            if (role == null)
                return BadRequest();
            return Ok(_tokenService.GenerateToken(role));
        }
    }
}
