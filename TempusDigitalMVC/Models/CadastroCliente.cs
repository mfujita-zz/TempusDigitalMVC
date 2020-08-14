using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //https://docs.microsoft.com/pt-br/aspnet/core/tutorials/razor-pages/da1?view=aspnetcore-3.1
        [DefaultValue(0)]
        [Column(TypeName = "double(8,2)")]
        [DataType(DataType.Currency)]
        public double RendaFamiliar { get; set; }
    }
}
