using Microsoft.AspNetCore.Mvc;

namespace TZ.Onelect.TestServer.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("/SendArray")]
        public IActionResult SendArray([FromBody]List<int> array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            return Ok("Correct");
        }
    }
}
