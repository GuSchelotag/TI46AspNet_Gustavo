using Microsoft.AspNetCore.Mvc;

namespace PrimeiroProjeto.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
