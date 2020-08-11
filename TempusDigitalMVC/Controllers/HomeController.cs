using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TempusDigitalMVC.Models;

namespace TempusDigitalMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private ContextoCadastro contextoCadastro;

        public HomeController(ContextoCadastro cc)
        {
            contextoCadastro = cc;
        }

        public IActionResult Index()
        {
            return View(contextoCadastro.CadastroCliente);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cadastro(CadastroCliente cadastroCliente)
        {
            if (ModelState.IsValid)
            {
                contextoCadastro.CadastroCliente.Add(cadastroCliente);
                contextoCadastro.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public IActionResult Listagem()
        {
            return View(contextoCadastro.CadastroCliente);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
