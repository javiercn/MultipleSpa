using Microsoft.AspNetCore.Mvc;

namespace MultipleSpa.Controllers;
public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }
}
