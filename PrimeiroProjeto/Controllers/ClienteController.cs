using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Data;
using PrimeiroProjeto.Models;
using System.Linq;
using X.PagedList.EF;

namespace PrimeiroProjeto.Controllers
{
    public class ClienteController : Controller
    {
        // injeção de dependência do contexto
        private readonly Context _context;

        // Construtor que recebe o contexto
        public ClienteController(Context context)
        {
            // Inicializa o contexto
            _context = context;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("Id,Nome,Telefone,Email,DataCadastro")] Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarClientes(string searchString, int? page)
        {
            var clientes = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(c => EF.Functions.Like(c.Nome, $"%{searchString}%"));
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var pagedList = await clientes.OrderBy(c => c.Nome).ToPagedListAsync(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            return View(pagedList);
        }
    }
}
