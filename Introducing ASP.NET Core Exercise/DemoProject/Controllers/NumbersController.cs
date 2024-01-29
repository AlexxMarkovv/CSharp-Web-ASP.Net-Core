using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    public class NumbersController : Controller
    {
        private readonly ILogger<NumbersController> _logger;

        public NumbersController(ILogger<NumbersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNums(int num)
        {
            return View(num);
        }

    }
}
