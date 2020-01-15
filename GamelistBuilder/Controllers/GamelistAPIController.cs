using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GamelistBuilder.Commands;
using MediatR;
using GamelistBuilder.Models;
using GamelistBuilder.ViewModels;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamelistBuilder.Controllers
{
    [Route("api/[controller]")]
    public class GamelistAPIController : Controller
    {
        private readonly IMediator mediator;
        public GamelistAPIController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Gamelist>> Get()
        {
            var response = await mediator.Send(new GetGamelists());
            return response;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<GamelistViewModel> Get(int id)
        {
            var response = await mediator.Send(new GetGamelist(id));
            return response;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
