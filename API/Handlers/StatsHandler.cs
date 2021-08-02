using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Dtos;
using API.Queries;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class StatsHandler : IRequestHandler<StatsQuery, IReadOnlyList<StatisticToReturnDto>>
    {
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public StatsHandler(IStatisticService statisticService,IMapper mapper)
        {
            _statisticService = statisticService;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<StatisticToReturnDto>> Handle(StatsQuery request, CancellationToken cancellationToken)
        {
            var stats = await _statisticService.GetStatisticAsync(request.AdOrderBySpec);

            if (stats.First() == null)
                return null;

            var statsToReturn =
                _mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticToReturnDto>>(stats);

            return (IReadOnlyList<StatisticToReturnDto>) statsToReturn;
        }
    }
}
