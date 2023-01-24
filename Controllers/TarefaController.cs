using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }


        //Implementado
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            //Implementado
            var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        //Implementado
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        //Implementado
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            //Implementado
             var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tarefa);
        }

        //Implementado
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }


        //Implementado
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        //Implementado
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Implementado
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        //Implementado
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

           else
           {
             tarefaBanco.Id = tarefa.Id;
             tarefaBanco.Titulo = tarefa.Titulo;
             tarefaBanco.Descricao = tarefa.Descricao;
             tarefa.Data = tarefa.Data;
             tarefa.Status = tarefa.Status;

             _context.Tarefas.Update(tarefaBanco);
             _context.SaveChanges();
             return Ok(tarefaBanco);
           }
            
        }

        // Implementado
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
            {
                return NotFound();

            }  // Implementado
            else
            {
                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();
                return NoContent();
            }
                

           
           
        }
    }
}
