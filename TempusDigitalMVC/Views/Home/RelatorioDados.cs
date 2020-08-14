using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempusDigitalMVC.Views.Home
{
    public class RelatorioDados
    {
        //https://stackoverflow.com/questions/11296634/pass-a-simple-string-from-controller-to-a-view-mvc3

        public double MediaRendaFamiliar { get; set; }
        public int QtdePessoaRendaAcimaMedia { get; set; }
        public int QtdeClasseA { get; set; }
        public int QtdeClasseB { get; set; }
        public int QtdeClasseC { get; set; }
        public double RendaMes { get; set; }
        public double RendaSemana { get; set; }
        public double RendaHoje { get; set; }
    }
}
