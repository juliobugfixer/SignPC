
using Microsoft.AspNetCore.Mvc;
using SignPC.Services;
using SignPC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SignPC.Controllers
{
    
    [Authorize]
    public class EquipaController : Controller
    {
        private AppDbContext _context;

        public EquipaController (AppDbContext context){
            _context = context;
        }

        //Metodo Listar Membros
        public IActionResult Index(string  pesquisaString)
        {
          var equipa =  _context.Equipa.ToList();

          if (!String.IsNullOrEmpty(pesquisaString)) 
          {
            equipa = equipa.Where(n =>n.Id.ToString().Contains(pesquisaString.ToString()) || n.NomeEquipa.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase)).ToList();

          }
          return View(equipa);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Adicionar()
        {
           return View();
        }

        //Metodo Adicionar Equipas
        [HttpPost]
        public async Task<IActionResult> Adicionar(Equipa equipa)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Add(equipa);
                    await _context.SaveChangesAsync();
                    TempData["Adicionar"] = "Equipa adicionada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao adicionar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Os campos são obrigatórios, preencha todos!");
            return View(equipa);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var equipa = _context.Equipa.Where(x => x.Id==id).FirstOrDefault();
            return View (equipa);
        }

        //Metodo Editar
        [HttpPost]
        public async Task<IActionResult> Alterar(Equipa equipa)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   var dado = _context.Equipa.Where(x => x.Id==equipa.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        dado.NomeEquipa=equipa.NomeEquipa;
                   }

                   await _context.SaveChangesAsync();
                    TempData["Alterar"] = "Equipa alterada com sucesso!";
                   return RedirectToAction("Index");
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Os campos são obrigatórios, preencha todos!");
            return View(equipa);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Deletar(int id){
            var dado = _context.Equipa.Where(x => x.Id==id).FirstOrDefault();
            return View(dado);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(Equipa equipa)
        {
                try
                {
                   var dado = _context.Equipa.Where(x => x.Id==equipa.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        _context.Remove(dado);
                        await _context.SaveChangesAsync();
                        TempData["Deletar"] = "Equipa deletada com sucesso!";
                        return RedirectToAction("Index");
                   }
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Está vazia!{ex.Message}");
                }
            ModelState.AddModelError(string.Empty,$"Erro ao deletar.");
            return View(equipa);
        }
    }       
}