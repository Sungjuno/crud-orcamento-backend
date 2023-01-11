using System;
namespace crud_orcamento.Models
{
	public class Orcamento
	{
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public string DescricaoDoServico { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

