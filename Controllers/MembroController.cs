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
    public class MembroController : Controller
    {
        private AppDbContext _context;

        public MembroController(AppDbContext context){
            _context = context;
        }

        //Metodo Listar Membros
        public IActionResult Index(string  pesquisaString)
        {
          var membros =  _context.Membro.ToList();
            if (!String.IsNullOrEmpty(pesquisaString)) 
            {
                membros = membros.Where(n => n.Id.ToString().Contains(pesquisaString.ToString()) || n.NumProcesso.Contains(pesquisaString)
                || n.NomeMembro.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) || n.Ano.ToString().Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) || n.Genero.ToString().Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) || n.Curso.ToString().Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) ||
                n.EmailMembro.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) || n.RefEquipa.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase)).ToList();

            }
          return View(membros);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Adicionar()
        {
           return View();
        }

        //Metodo Adicionar Membros
        [HttpPost]
        public async Task<IActionResult> Adicionar(Membro membro)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Add(membro);
                    await _context.SaveChangesAsync();
                    TempData["Adicionar"] = "Membro adicionado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao adicionar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Preencha os campos!");
            return View(membro);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var membrodado = _context.Membro.Where(x => x.Id==id).FirstOrDefault();
            return View (membrodado);
        }

        //Metodo Editar
        [HttpPost]
        public async Task<IActionResult> Alterar(Membro membro)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   var dado = _context.Membro.Where(x => x.Id==membro.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        dado.NumProcesso=membro.NumProcesso;
                        dado.NomeMembro=membro.NomeMembro;
                        dado.Genero=membro.Genero;
                        dado.EmailMembro=membro.EmailMembro;
                        dado.Curso=membro.Curso;
                        dado.Ano=membro.Ano;
                        dado.Telefone=membro.Telefone;
                        dado.RefEquipa=membro.RefEquipa;
                   }

                   await _context.SaveChangesAsync();
                   TempData["Alterar"] = "Membro alterado com sucesso!";
                   return RedirectToAction("Index");
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Preencha os campos!");
            return View(membro);
        }

         //Metodo Pegar
        [HttpGet]
        public IActionResult Deletar(int id){
            var dado = _context.Membro.FirstOrDefault(x => x.Id==id);
            return View(dado);
        }

        //Metodo Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(Membro membro)
        {
                try
                {
                   var dado = _context.Membro.FirstOrDefault(x => x.Id==membro.Id);
                   if(dado != null)
                   {
                        _context.Remove(dado);
                        await _context.SaveChangesAsync();
                        TempData["Deletar"] = "Membro deletado com sucesso!";
                        return RedirectToAction("Index");
                   }
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Está vazia{ex.Message}");
                }
            ModelState.AddModelError(string.Empty,$"Erro ao deletar.");
            return View(membro);
        }
    }
}