using System;
using crud_orcamento.Database;
using crud_orcamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace crud_orcamento.Controllers
{
    [Route("orcamentos")]
	public class OrcamentoController : ControllerBase 
   
	{

        private OrcamentoContext _orcamento;

        public OrcamentoController(OrcamentoContext orcamento)
        {
            _orcamento = orcamento;
        }

        [HttpGet("")]
        public IEnumerable<Orcamento> Index()
            //[FromQuery] int skip = 0, [FromQuery] int take = 4
        {
            //var lista = new List<Orcamento>();
            //using (var conn = new MySqlConnection("Server=localhost;Database=OrcamentoDatabase;Uid=root;Pwd=sung87ju;"))
            //{
            //    conn.Open();
            //    var query = $"select * from Orcamentos";

            //    var command = new MySqlCommand(query, conn);
            //    var dr = await command.ExecuteReaderAsync();
            //    while (dr.Read())
            //    {
            //        lista.Add(new Orcamento
            //        {
            //            Id = Convert.ToInt32(dr["id"]),
            //            ClienteId = Convert.ToInt32(dr["ClienteId"]),
            //            FornecedorId = Convert.ToInt32(dr["FornecedorId"]),
            //            Telefone = dr["telefone"].ToString() ?? "",
            //            Email = dr["email"].ToString() ?? "",
            //            Endereco = dr["endereco"].ToString() ?? "",
            //        });
            //    }

            //    conn.Close();
            //}

            //return lista;
            //return StatusCode(200, _orcamento);
            return _orcamento.Orcamentos;
        }


        [HttpGet("{id}")]
        public IActionResult Details([FromRoute] int id)
        {
            var orca = _orcamento.Orcamentos.FirstOrDefault(o => o.Id == id);
            if (orca == null) return NotFound();
            return StatusCode(200,orca);

            //return StatusCode(200, _orcamento);
        }


        // POST: Clientes
        [HttpPost("")]
        public IActionResult Create([FromBody] Orcamento orcamento)
        {
            _orcamento.Orcamentos.Add(orcamento);
            _orcamento.SaveChanges();
            //var cliente = BuilderServico<Cliente>.Builder(clienteDTO);
            //await _servico.IncluirAsync(cliente);
            return StatusCode(200, orcamento);
        }


        // PUT: Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Orcamento orcamento)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=OrcamentoDatabase;Uid=root;Pwd=sung87ju;"))
            {
                conn.Open();
                var query = $"update Orcamentos set ClienteId=@ClienteId,FornecedorId=@FornecedorId,DescricaoDoServico=@DescricaoDoServico,ValorTotal=@ValorTotal, DataCriacao=@DataCriacao where id = @id;";
                var command = new MySqlCommand(query, conn);
                command.Parameters.Add(new MySqlParameter("@Id", orcamento.Id));
                command.Parameters.Add(new MySqlParameter("@ClienteId", orcamento.ClienteId));
                command.Parameters.Add(new MySqlParameter("@FornecedorId", orcamento.FornecedorId));
                command.Parameters.Add(new MySqlParameter("@DescricaoDoServico", orcamento.DescricaoDoServico));
                command.Parameters.Add(new MySqlParameter("@ValorTotal", orcamento.ValorTotal));
                command.Parameters.Add(new MySqlParameter("@DataCriacao", orcamento.DataCriacao));
                await command.ExecuteNonQueryAsync();

                conn.Close();
            }

            //return cliente;
            //if (id != cliente.Id)
            //{
            //    return StatusCode(400, new
            //    {
            //        Mensagem = "O Id do cliente precisa bater com o id da URL"
            //    });
            //}

            //var clienteDb = await _servico.AtualizarAsync(cliente);

            return StatusCode(200, orcamento);
        }

        // POST: Clientes/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var orca = _orcamento.Orcamentos.FirstOrDefault(o => o.Id == id);
            if (orca == null) return NotFound();
            _orcamento.Remove(orca);
            _orcamento.SaveChanges();
            
            
            //var clienteDb = (await _servico.TodosAsync()).Find(c => c.Id == id);
            //if (clienteDb is null)
            //{
            //    return StatusCode(404, new
            //    {
            //        Mensagem = "O cliente informado não existe"
            //    });
            //}

            //await _servico.ApagarAsync(clienteDb);

            return StatusCode(204);
        }
    }
}

