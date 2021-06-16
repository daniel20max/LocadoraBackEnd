using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Models
{
    public class Locacao
    {
        public int id { get; set; }

        [Display(Name = "Data da locacao")]
        public DateTime? dataHora { get; set; }

        [Required(ErrorMessage = "Informe o tempo de locacao desejada")]
        [Display(Name = "Data de entrega")]
        public DateTime? alguel { get; set; }

        [Required(ErrorMessage = "Informe o filme para alocar!")]
        [Display(Name = "Filme")]
        public int? filmeId { get; set; }

        public Filme filme
        {
            get; set;
        }

    }
}
