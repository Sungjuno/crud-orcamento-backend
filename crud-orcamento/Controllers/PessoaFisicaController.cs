using System;
using crud_orcamento.Database;
using crud_orcamento.DTOs;
using crud_orcamento.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace crud_orcamento.Controllers
{
	[Route("pessoafisica")]
	public class PessoaFisicaController : ControllerBase
	{
		private OrcamentoContext _pessoaFisica;

        public PessoaFisicaController(OrcamentoContext pessoaFisica)
        {
            _pessoaFisica = pessoaFisica;
        }

        [HttpGet("")]
        public IEnumerable<PessoaFisica> Index()
        {
            return _pessoaFisica.PessoasFisicas;
        }

        [HttpGet("{id}")]
        public PessoaFisica? RecuperaPorId(int id)
        {
            return _pessoaFisica.PessoasFisicas.FirstOrDefault(pf => pf.Id == id);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] PessoaFisicaDTO pessoaFisicaDTO)
        {

            PessoaFisica pessoaFisica = new PessoaFisica
            {
                Nome = pessoaFisicaDTO.Nome,
                Telefone = pessoaFisicaDTO.Telefone,
                CPF = pessoaFisicaDTO.CPF,
            };

            _pessoaFisica.PessoasFisicas.Add(pessoaFisica);
            _pessoaFisica.SaveChanges();
            return StatusCode(200, pessoaFisica);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody]PessoaFisica pessoaFisica)
        {
            //_pessoaFisica.PessoasFisicas.FirstOrDefault(pf => pf.Id == id);
            using (var conn = new MySqlConnection("Server=localhost;Database=OrcamentoDatabase;Uid=root;Pwd=sung87ju;"))
            {
                conn.Open();
                var query = $"update PessoasFisicas set Id=@Id,Nome=@Nome,Telefone=@Telefone,CPF=@CPF where id = @id;";
                var command = new MySqlCommand(query, conn);
                command.Parameters.Add(new MySqlParameter("@Id", pessoaFisica.Id));
                command.Parameters.Add(new MySqlParameter("@Nome", pessoaFisica.Nome));
                command.Parameters.Add(new MySqlParameter("@Telefone", pessoaFisica.Telefone));
                command.Parameters.Add(new MySqlParameter("@CPF", pessoaFisica.CPF));
                //command.Parameters.Add(new MySqlParameter("@DataCriacao", pessoaFisica.DataCriacao));
                await command.ExecuteNonQueryAsync();

                conn.Close();
            }

            return StatusCode(200, pessoaFisica);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
           var pf = _pessoaFisica.PessoasFisicas.FirstOrDefault(pf => pf.Id == id);
            if (pf == null) return NotFound();
            _pessoaFisica.Remove(pf);
            _pessoaFisica.SaveChanges();
            return StatusCode(204);
        }
    }
}

