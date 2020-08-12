using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempusDigitalMVC.Models;

namespace TempusDigitalMVC.Validacao
{
    public class ValidaEntrada : Attribute //, IModelValidator
    {
        public double renda { get; set; }

        public double Validate(ModelValidationContext contexto)
        {
            if (renda.ToString().Contains(','))
                renda = Convert.ToDouble(renda.ToString().Replace(',', '.'));
            return renda;
        }
    }
}
