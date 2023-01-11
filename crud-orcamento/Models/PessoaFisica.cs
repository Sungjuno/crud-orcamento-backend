using System;
namespace crud_orcamento.Models
{
	public class PessoaFisica
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int CPF { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}

