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
    [Route("api/Cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> Get ([FromServices] DataContext context)
        {
            var clientes = await context.Clientes.ToListAsync();
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", clientes));
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<ActionResult<Cliente>> GetById([FromServices] DataContext context, int id)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(new ResponseViewModel(true, "Consulta realizada com sucesso!", cliente));
        }

        //Nessário informar apenas nome e CPF do cliente
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post([FromServices] DataContext context, [FromBody] Cliente model) 
        {
            if (ModelState.IsValid)
            {
                var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Cpf == model.Cpf);

                if (cliente != null)
                {
                    return BadRequest(new ResponseViewModel(true, "CPF já cadastrado!", cliente));
                }
                else
                {
                    model.Ativo = true;

                    context.Clientes.Add(model);
                    await context.SaveChangesAsync();
                    return Ok(new ResponseViewModel(true, "Inclusão realizada com sucesso!", model));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Atualiza dados do cliente, incluindo a desativação
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Cliente>> Put ([FromServices] DataContext context, [FromBody] Cliente model)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Update(model);
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
