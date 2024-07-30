using Microsoft.AspNetCore.Mvc;

namespace TG_Bot_Template.Controllers
{
    /// <summary>
    /// Example controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        /// <summary>
        /// Just example endpoing
        /// </summary>
        /// <returns></returns>
        [HttpGet("ping")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Pong()
        {
            return Ok("Pong!");
        }
    }
}
