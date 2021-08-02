using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace API.Commands
{
    public class AdClickCommand : IRequest<int>
    {
        public AdClickCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
