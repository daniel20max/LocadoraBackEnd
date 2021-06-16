using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Models
{
    public class Filme
    {

        [Display(Name = "#")]
        public int id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Filme")]
        [Display(Name = "Nome do Filme")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Digite uma sinopse para o Filme")]
        public string sinopse { get; set; }

        [Required(ErrorMessage = "Digite o valor do Filme")]
        public double? preco { get; set; }

        [Required(ErrorMessage = "Digite a data de lancamente do Filme")]
        [Display(Name = "Data do Lançamento")]
        [DataType(DataType.Date)]
        public DateTime? lancamento { get; set; }

        [Required(ErrorMessage = "Digite o tempo de duracao do Filme")]
        [Display(Name = "Duração")]
        public double? duracao { get; set; }
        [Required(ErrorMessage = "Seu Filme Precisa de um Link")]
        [Display(Name = "Link direto")]
        public string link { get; set; }

        public List<Locacao> locacaos { get; set; }
        public Filme(int id, string nome, string sinopse, double preco, DateTime lancamento, double duracao)
        {
            this.id = id;
            this.nome = nome;
            this.sinopse = sinopse;
            this.preco = preco;
            this.lancamento = lancamento;
            this.duracao = duracao;
        }
        public Filme()
        {

        }
        
    }

}
