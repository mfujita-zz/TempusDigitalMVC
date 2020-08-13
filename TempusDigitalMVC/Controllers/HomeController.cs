using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
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
                cadastroCliente.RendaFamiliar = Convert.ToDouble(string.Format(new System.Globalization.CultureInfo("pt-br"), "{0:N2}", cadastroCliente.RendaFamiliar));
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

        public IActionResult Update(int id)
        {
            return View(contextoCadastro.CadastroCliente.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(CadastroCliente cadastro)
        {
            contextoCadastro.CadastroCliente.Update(cadastro);
            contextoCadastro.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cadastro = contextoCadastro.CadastroCliente.Where(x => x.Id == id).FirstOrDefault();
            contextoCadastro.CadastroCliente.Remove(cadastro);
            contextoCadastro.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Relatorio()
        //{
        //    Sql
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
