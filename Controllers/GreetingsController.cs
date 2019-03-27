using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> HelloWorld()
        {
            return new string[] { "greeting", "Hello World!" };
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<string>> HelloUser(string name)
        {
            return new string[] { "greeting", $"Hello {name}!!" };
        }

    }
}