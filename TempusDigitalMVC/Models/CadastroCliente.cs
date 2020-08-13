using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TempusDigitalMVC.Models
{
    public class CadastroCliente
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(10)]
        public string CPF { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [DefaultValue(0)]
        public double RendaFamiliar { get; set; }
    }
}
