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
    [Route("api/Locacao")]
    public class LocacaoController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Locacao>>> Get([FromServices] DataContext context)
        {
            var locacoes = await context.Locacoes.ToListAsync();
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", locacoes));
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Locacao>> GetById([FromServices] DataContext context, int id)
        {
            var locacao = await context.Locacoes.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", locacao)); 
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Locacao>> Post([FromServices] DataContext context, [FromBody] Locacao model)
        {
            //Necessário informar apenas Id do Cliente e Id do Livro
            if (ModelState.IsValid)
            {
                var locacao = await context.Locacoes.FirstOrDefaultAsync(x => x.IdFilme == model.IdFilme);
                var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == model.IdCliente);
                var filme = await context.Filmes.FirstOrDefaultAsync(x => x.Id == model.IdFilme);

                if (locacao != null || cliente.Ativo == false || filme.Ativo == false)
                {
                    if (locacao != null && locacao.StatusLocacao == true)
                    {
                        return BadRequest("Este filme encontra-se alugado no momento!");
                    }
                    if (cliente.Ativo == false)
                    {
                        return BadRequest("Este cliente encontra-se desativado no momento e não pode alugar um filme!");
                    }
                    if (filme.Ativo == false)
                    {
                        return BadRequest("Este filme encontra-se desativado no momento e não pode ser alugado!");
                    }
                    else
                    {
                        model.StatusLocacao = true;
                        model.DataLocacao = DateTime.Now;
                        model.DataDevolucao = null;

                        context.Locacoes.Add(model);
                        await context.SaveChangesAsync();
                        return Ok(new ResponseViewModel(true, "Inclusão realizada com sucesso!", model));
                    }
                }
                else
                {
                    model.StatusLocacao = true;
                    model.DataLocacao = DateTime.Now;
                    model.DataDevolucao = null;

                    context.Locacoes.Add(model);
                    await context.SaveChangesAsync();
                    return Ok(new ResponseViewModel(true, "Inclusão realizada com sucesso!", model));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Locacao>> Put([FromServices] DataContext context, [FromBody] Locacao model)
        {
            if (ModelState.IsValid)
            {
                var locacao = await context.Locacoes.FirstOrDefaultAsync(x => x.Id == model.Id);

                locacao.StatusLocacao = false;
                locacao.DataDevolucao = DateTime.Now;

                TimeSpan duration = (TimeSpan)(locacao.DataDevolucao - locacao.DataLocacao);

                context.Locacoes.Update(locacao);
                await context.SaveChangesAsync();

                if (duration.Days > 15)
                {
                    return Ok(new ResponseViewModel(true, "Prazo de devolução de 15 dias expirado!", locacao));
                }
                else
                {
                    return Ok(new ResponseViewModel(true, "Alteração Realizada com sucesso!", locacao));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
