using Microsoft.AspNetCore.Mvc;
using CarCleanz.Data;
using CarCleanz.Models;


namespace CarCleanz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }


}