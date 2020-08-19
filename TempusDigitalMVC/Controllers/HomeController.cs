using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TempusDigitalMVC.Models;
using TempusDigitalMVC.Views.Home;
using TempusDigitalMVC.Infra;

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
                var cpfValido = ValidaCPF.IsCpf(cadastroCliente.CPF);

                if (!cpfValido)
                {
                    ModelState.AddModelError("CPF", "CPF inválido.");
                }

                var jaExisteCpf = contextoCadastro.CadastroCliente
                    .Any(c => c.CPF == cadastroCliente.CPF);

                if (jaExisteCpf)
                {
                    ModelState.AddModelError("Cpf", "CPF já cadastrado.");
                    return View(cadastroCliente);
                }

                contextoCadastro.CadastroCliente.Add(cadastroCliente);
                contextoCadastro.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
 //https://stackoverflow.com/questions/21296281/mvc-razor-validation-errors-showing-on-page-load-when-no-data-has-been-posted
                //ModelState.Clear();
                return View();
            }
        }

        public IActionResult Listagem()
        {
            return View(contextoCadastro.CadastroCliente);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(contextoCadastro.CadastroCliente.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(CadastroCliente cadastroCliente)
        {
            var DataCadastroOriginal = contextoCadastro.CadastroCliente.Where(x => x.Id == cadastroCliente.Id)
                .Select(p => p.DataCadastro).FirstOrDefault();

            var cpfValido = ValidaCPF.IsCpf(cadastroCliente.CPF);

            if (!cpfValido)
            {
                ModelState.AddModelError("CPF", "CPF inválido.");
            }

            cadastroCliente.DataCadastro = DataCadastroOriginal;
            contextoCadastro.CadastroCliente.Update(cadastroCliente);
            contextoCadastro.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cadastro = contextoCadastro.CadastroCliente.Where(x => x.Id == id).FirstOrDefault();

            if (cadastro == null)
                return NotFound();

            contextoCadastro.CadastroCliente.Remove(cadastro);
            contextoCadastro.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Relatorio(string filtro)
        {
            RelatorioDados dados = new RelatorioDados();

            //1º card
            dados.MediaRendaFamiliar = (decimal)contextoCadastro.CadastroCliente.Average(x => x.RendaFamiliar);
            int AnoMaiorIdade = DateTime.Now.Year - 18;
            dados.QtdePessoaRendaAcimaMedia = contextoCadastro.CadastroCliente.Count(x => x.RendaFamiliar > dados.MediaRendaFamiliar && x.DataNascimento.Year > AnoMaiorIdade);
            

            //2º card
            dados.QtdeClasseA = contextoCadastro.CadastroCliente.Count(x => x.RendaFamiliar <= 980);
            dados.QtdeClasseB = contextoCadastro.CadastroCliente.Count(x => x.RendaFamiliar > 980 && x.RendaFamiliar <= 2500);
            dados.QtdeClasseC = contextoCadastro.CadastroCliente.Count(x => x.RendaFamiliar > 2500);

            //3º card
            if (filtro == "mes")
            {
                dados.RendaMes = (decimal)contextoCadastro.CadastroCliente.Where(x => x.DataCadastro.Month == DateTime.Now.Month).Sum(p => p.RendaFamiliar);
            }

            if (filtro == "semana")
            {
                //https://stackoverflow.com/questions/33407470/linq-lambda-get-all-the-records-for-this-week
                var InicioDiaSemana = DateTime.Today;
                var FimDiaSemana = DateTime.Today.AddDays(1);
                var DiaSemanaHoje = DateTime.Now.DayOfWeek;
                if ((int)DiaSemanaHoje == 0)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(0);
                }
                else if ((int)DiaSemanaHoje == 1)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-1);
                }
                else if ((int)DiaSemanaHoje == 2)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-2);
                }
                else if ((int)DiaSemanaHoje == 3)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-3);
                }
                else if ((int)DiaSemanaHoje == 4)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-4);
                }
                else if ((int)DiaSemanaHoje == 5)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-5);
                }
                else if ((int)DiaSemanaHoje == 6)
                {
                    InicioDiaSemana = DateTime.Today.AddDays(-6);
                }

                FimDiaSemana = DateTime.Today;

                dados.RendaSemana = (decimal)contextoCadastro.CadastroCliente.Where(x => x.DataCadastro >= InicioDiaSemana && x.DataCadastro <= FimDiaSemana)
                    .Sum(p => p.RendaFamiliar);
            }

            if (filtro == "hoje")
            {
                dados.RendaHoje = (decimal)contextoCadastro.CadastroCliente.Where(x => x.DataCadastro == DateTime.Today).Sum(z => z.RendaFamiliar);
            }

            return View(dados);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
