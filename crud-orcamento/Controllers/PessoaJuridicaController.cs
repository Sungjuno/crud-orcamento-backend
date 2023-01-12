using System;
using crud_orcamento.Database;
using crud_orcamento.DTOs;
using crud_orcamento.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace crud_orcamento.Controllers
{
	[Route("pessoajuridica")]
	public class PessoaJuridicaController : ControllerBase
	{
		private OrcamentoContext _pessoaJuridica;

		public PessoaJuridicaController(OrcamentoContext pessoaJuridica)
		{
			_pessoaJuridica = pessoaJuridica;
		}

        [HttpGet("")]
        public IEnumerable<PessoaJuridica> Index()
        {
            return _pessoaJuridica.PessoasJuridicas;
        }

        [HttpGet("{id}")]
        public PessoaJuridica? RecuperaPorId(int id)
        {
            return _pessoaJuridica.PessoasJuridicas.FirstOrDefault(pf => pf.Id == id);
        }

        [HttpPost("")]
        //public IActionResult Create([FromBody] PessoaFisicaDTO pessoaFisicaDTO)
        //{

        //    PessoaJuridica pessoaJuridica = new PessoaJuridica
        //    {
        //        Nome = pessoaFisicaDTO.Nome,
        //        Telefone = pessoaFisicaDTO.Telefone,
        //        CPF = pessoaFisicaDTO.CPF,
        //    };

        //    _pessoaFisica.PessoasFisicas.Add(pessoaFisica);
        //    _pessoaFisica.SaveChanges();
        //    return StatusCode(200, pessoaFisica);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PessoaFisica pessoaFisica)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=OrcamentoDatabase;Uid=root;Pwd=sung87ju;"))
            {
                conn.Open();
                var query = $"update PessoasFisicas set Id=@Id,Nome=@Nome,Telefone=@Telefone,CPF=@CPF where id = @id;";
                var command = new MySqlCommand(query, conn);
                command.Parameters.Add(new MySqlParameter("@Id", pessoaFisica.Id));
                command.Parameters.Add(new MySqlParameter("@Nome", pessoaFisica.Nome));
                command.Parameters.Add(new MySqlParameter("@Telefone", pessoaFisica.Telefone));
                command.Parameters.Add(new MySqlParameter("@CPF", pessoaFisica.CPF));
                await command.ExecuteNonQueryAsync();

                conn.Close();
            }

            return StatusCode(200, pessoaFisica);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var pj = _pessoaJuridica.PessoasJuridicas.FirstOrDefault(pj => pj.Id == id);
            if (pj == null) return NotFound();
            _pessoaJuridica.Remove(pj);
            _pessoaJuridica.SaveChanges();
            return StatusCode(204);
        }

    }
}

