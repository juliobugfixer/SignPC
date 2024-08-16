using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignPC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;


namespace SignPC.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    [Route("StatusCodeError/{statusCode}")]
    public IActionResult Error(int statusCode)
    {
        if(statusCode == 404)
        {
            ViewBag.ErrorMessage = "404 - Página não encontrada!";
        }
        return View("Index");
    }

    public IActionResult Error()
    {
        return View();
    }
}
