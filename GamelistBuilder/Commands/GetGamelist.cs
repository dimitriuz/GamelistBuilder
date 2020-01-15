using GamelistBuilder.Models;
using GamelistBuilder.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Commands
{
    public class GetGamelist:IRequest<GamelistViewModel>
    {
        public int Id { get; set; }

        public GetGamelist(int _id)
        {
            Id = _id;
        }
    }
}
