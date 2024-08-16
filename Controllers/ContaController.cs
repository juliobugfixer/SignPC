using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignPC.Models;
using SignPC.Entities;
using SignPC.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SignPC.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;

        public ContaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string pesquisaString)
        {
  
            var usuarios = _context.UsuarioContas.ToList();

            if (!string.IsNullOrEmpty(pesquisaString))
            {
                usuarios = usuarios.Where(u => u.Id.ToString().Contains(pesquisaString.ToString()) || u.Nome.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) || u.Email.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(usuarios);
        }

        
        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registro(AdminRegistro model)
        {
            if (ModelState.IsValid)
            {
                UsuarioConta conta = new UsuarioConta();
                conta.Nome = model.Nome;
                conta.Email = model.Email;
                conta.Senha = model.Senha;

                try
                {
                _context.UsuarioContas.Add(conta);
                _context.SaveChanges();
                ModelState.Clear();
                TempData["Sucesso"] = $"{model.Email} registrado com sucesso!";
                }
                catch (DbUpdateException ex)
                {
                
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                return View(model);
                }
                return RedirectToAction("Login", "Conta");
            }
            return View(model);
        }

        //View pra deletar
        public IActionResult Deletar()
        {
            return View();
        }


        //Metodo Pegar pra deletar
        [HttpGet]
        public IActionResult Deletar(int id){
            var dado = _context.UsuarioContas.Where(x => x.Id==id).FirstOrDefault();
            return View(dado);
        }

        //Metodo Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(UsuarioConta user)
        {
                try
                {
                   var dado = _context.UsuarioContas.Where(x => x.Id==user.Id).FirstOrDefault();
                   if(dado != null)
                   {                   
                        _context.Remove(dado);
                        await _context.SaveChangesAsync();
                        ModelState.Clear();
                        TempData["Deletar"] = "Usuário deletado com sucesso!";
                        return RedirectToAction("Index");
                   }
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Está vazia!{ex.Message}");
                }
            ModelState.AddModelError(string.Empty,$"Erro  ao deletar.");
            return View(user);
        }

        //Metodo pegar pra editar
        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var dado = _context.UsuarioContas.Where(x => x.Id==id).FirstOrDefault();
            return View(dado);
        }


        //Metodo Editar
        [HttpPost]
        public async Task<IActionResult> Alterar(UsuarioConta user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   var dado = _context.UsuarioContas.Where(x => x.Id==user.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        dado.Nome=user.Nome;
                        dado.Email=user.Email;
                        dado.Senha=user.Senha;
                   }

                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    TempData["Alterar"] = "Usuário alterado com sucesso!";
                   return RedirectToAction("Index");
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Preencha os campos!");
            return View(user);
        }


        //Metodo Manter Logado
        public IActionResult Login() {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        //Metodo de Verificacao e auth
        [HttpPost]        
        public async Task<IActionResult> Login(AdminLogin model) 
        {

            if (ModelState.IsValid)
            {
                var user = _context.UsuarioContas.Where(x => x.Email == model.Email && x.Senha == model.Senha).FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim(ClaimTypes.Role, "Permissao"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                    };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Credencias incorretas! Garanta que a senha e o e-mail estejam corretas.");
                    return View();
                }
            }
            return View(model);
        }

        public IActionResult Logout() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Conta");
        }
    }
}
    
