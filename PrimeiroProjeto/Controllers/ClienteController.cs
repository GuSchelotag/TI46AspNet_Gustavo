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

       
        public IActionResult Index()
        {
            return View();
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
                if (cliente.Id == 0)
                    _context.Add(cliente);
                else
                    _context.Update(cliente);

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

            var pagedList = await clientes.OrderBy(c => c.Id).ToPagedListAsync(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            return View(pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(int id)
        {
            //await _context.Clientes.Where(x => x.Id == id).FirstAsync();
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View("Cadastrar", cliente);
        }
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("BuscarClientes", "Cliente");
        }

       
    }
}
