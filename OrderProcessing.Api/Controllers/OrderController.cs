using Microsoft.AspNetCore.Mvc;

namespace OrderProcessing.Api.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
