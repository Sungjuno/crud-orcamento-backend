using System;
using crud_orcamento.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_orcamento.Database
{
	public class OrcamentoContext : DbContext
	{
        public OrcamentoContext(DbContextOptions<OrcamentoContext> options) : base(options) { }

        public DbSet<Orcamento> Orcamentos { get; set; } = default!;
        public DbSet<PessoaFisica> PessoasFisicas { get; set; } = default!;
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; } = default!;
    }
}

