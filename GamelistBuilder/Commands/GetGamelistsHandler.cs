using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamelistBuilder.Commands
{
    public class GetGamelistsHandler : IRequestHandler<GetGamelists, IEnumerable<Gamelist>>
    {
        private IRepository<Gamelist> _repository;
        public GetGamelistsHandler(IRepository<Gamelist> repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<Gamelist>> Handle(GetGamelists request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}
