using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testelocadora.Data;
using testelocadora.Dtos;
using testelocadora.Models;

namespace testelocadora.Controllers
{
    [ApiController]
    [Route("api/Filme")]
    public class FilmeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Filme>>> Get([FromServices] DataContext context)
        {
            var filmes = await context.Filmes.ToListAsync();
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", filmes));
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Filme>> GetById([FromServices] DataContext context, int id)
        {
            var filme = await context.Filmes.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", filme));
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Filme>> Post([FromServices] DataContext context, [FromBody] Filme model)
        {
            if (ModelState.IsValid)
            {
                context.Filmes.Add(model);
                await context.SaveChangesAsync();
                return Ok(new ResponseViewModel(true, "Inclusão realizada com sucesso!", model));
            }
            else
            {
                model.Ativo = true;
                return BadRequest(ModelState);
            }
        }

        //Atualiza dados do filme, incluindo a desativação
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Filme>> Put([FromServices] DataContext context, [FromBody] Filme model)
        {
            if (ModelState.IsValid)
            {
                context.Filmes.Update(model);
                await context.SaveChangesAsync();
                return Ok(new ResponseViewModel(true, "Alteração Realizada com sucesso!", model));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
