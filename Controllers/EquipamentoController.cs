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
    public class EquipamentoController : Controller
    {
        private AppDbContext _context;

        public EquipamentoController (AppDbContext context){
            _context = context;
        }

        //Metodo Listar Equipas
        public IActionResult Index(string  pesquisaString)
        {
          var equipamento =  _context.Equipamento.ToList();
            if (!String.IsNullOrEmpty(pesquisaString)) 
            {
            equipamento = equipamento.Where(n => n.Id.ToString().Contains(pesquisaString.ToString()) || n.NomeEquipamento.Contains(pesquisaString, StringComparison.OrdinalIgnoreCase) 
            ||n.Estado.ToString().Contains(pesquisaString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
          return View(equipamento);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Adicionar()
        {
           return View();
        }

        //Metodo Adicionar Equipas
        [HttpPost]
        public async Task<IActionResult> Adicionar(Equipamento equipamento)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Add(equipamento);
                    await _context.SaveChangesAsync();
                    TempData["Adicionar"] = "Equipamento adicionado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao adicionar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Preencha os campos!");
            return View(equipamento);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var equipamento = _context.Equipamento.Where(x => x.Id==id).FirstOrDefault();
            return View (equipamento);
        }

        //Metodo Editar
        [HttpPost]
        public async Task<IActionResult> Alterar(Equipamento equipamento)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   var dado = _context.Equipamento.Where(x => x.Id==equipamento.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        dado.NomeEquipamento=equipamento.NomeEquipamento;
                        dado.Tipo=equipamento.Tipo;
                        dado.Descricao=equipamento.Descricao;
                        dado.Estado=equipamento.Estado;
                   }

                   await _context.SaveChangesAsync();
                    TempData["Alterar"] = "Equipamento alterado com sucesso!";
                   return RedirectToAction("Index");
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty,$"Preencha os campos!");
            return View(equipamento);
        }

        //Metodo Pegar
        [HttpGet]
        public IActionResult Deletar(int id){
            var dado = _context.Equipamento.Where(x => x.Id==id).FirstOrDefault();
            return View(dado);
        }

        //Metodo Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(Equipamento equipamento)
        {
                try
                {
                   var dado = _context.Equipamento.Where(x => x.Id==equipamento.Id).FirstOrDefault();
                   if(dado != null)
                   {
                        _context.Remove(dado);
                        await _context.SaveChangesAsync();
                        TempData["Deletar"] = "Equipamento deletado com sucesso!";
                        return RedirectToAction("Index");
                   }
                }

                catch (Exception ex) 
                {
                    ModelState.AddModelError(string.Empty,$"Erro  ao alterar{ex.Message}");
                }
            ModelState.AddModelError(string.Empty,$"Erro ao deletar.");
            return View(equipamento);
        }
    }
}