using GamelistBuilder.Infrastructure;
using GamelistBuilder.Models;
using GamelistBuilder.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GamelistBuilder.Commands
{
    public class GetGamelistHandler : IRequestHandler<GetGamelist, GamelistViewModel>
    {
        private IRepository<Gamelist> _repository;
        public GetGamelistHandler(IRepository<Gamelist> repository)
        {
            _repository = repository;
        }
        public Task<GamelistViewModel> Handle(GetGamelist request, CancellationToken cancellationToken)
        {
            var data = _repository.GetById(request.Id);
            var dataVM = new GamelistViewModel
            {
                Description = data.Description,
                GamesDirectory = data.GamesDirectory,
                VideoDirectory = data.VideoDirectory,
                PlatformId = data.Platform.Id,
                ImagesDirectory = data.ImagesDirectory,
                MarqueDirectory = data.MarqueDirectory,
                Name = data.Name,
                Path = data.Path
            };

            return Task.FromResult(dataVM);
        }
    }
}
