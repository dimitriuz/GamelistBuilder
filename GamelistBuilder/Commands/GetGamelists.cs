using GamelistBuilder.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamelistBuilder.Commands
{
    public class GetGamelists:IRequest<IEnumerable<Gamelist>>
    {

    }
}
