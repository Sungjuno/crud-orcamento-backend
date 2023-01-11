using System;
namespace crud_orcamento.Models
{
	public class PessoaJuridica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Telefone { get; set; }
        public int CNPJ { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}

