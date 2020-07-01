using Microsoft.AspNetCore.Mvc;


namespace matrixYT.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}