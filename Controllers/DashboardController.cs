using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignPC.Models;
using SignPC.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace SignPC.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        var contEquipamento = _context.Equipamento.Count();
        ViewBag.ContEquipamento = contEquipamento;

        var contEquipa = _context.Equipa.Count();
        ViewBag.ContEquipa = contEquipa;

        var contMembro = _context.Membro.Count();
        ViewBag.ContMembro = contMembro;

        return View();
    }

}
