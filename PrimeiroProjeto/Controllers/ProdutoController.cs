using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Data;
using PrimeiroProjeto.Models;
using System.Linq;
using X.PagedList;
using X.PagedList.EF;

namespace PrimeiroProjeto.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Context _context;

        public ProdutoController(Context context)
        {
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
        public async Task<IActionResult> Cadastrar([Bind("Id,Nome,Quantidade,Categoria,Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Id == 0)
                    _context.Add(produto);
                else
                    _context.Update(produto);

                await _context.SaveChangesAsync();
                return RedirectToAction("BuscarProdutos", "Produto");
            }

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> BuscarProdutos(string searchString, int? page)
        {
            var produtos = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(p => EF.Functions.Like(p.Nome, $"%{searchString}%"));
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var pagedList = await produtos.OrderBy(p => p.Id).ToPagedListAsync(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            return View(pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View("Cadastrar", produto);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("BuscarProdutos", "Produto");
        }
    }
}
