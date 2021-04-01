using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ReadModels;
using Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public static class GetCitizens
    {
        public class Query : IRequest<IEnumerable<CitizenReadModel>>
        {
            public Query(int pageIndex = 0, int pageSize = 25)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
            }

            public int PageIndex { get; }
            public int PageSize { get; }
        }

        internal class Handler : QueryHandler<Query, IEnumerable<CitizenReadModel>>
        {
            private readonly ILogger<QueryHandler<Query, IEnumerable<CitizenReadModel>>> _logger;
            private readonly ICitizenQueries _queries;
            private readonly IUniverseService _universeService;

            public Handler(ILogger<QueryHandler<Query, IEnumerable<CitizenReadModel>>> logger, ICitizenQueries queries, IUniverseService universeService) : base(logger)
            {
                _logger = logger;
                _queries = queries;
                _universeService = universeService;
            }

            protected override async Task<IEnumerable<CitizenReadModel>> Process(Query query)
            {
                // This query is done for inter service communication testing purposes
                var currentUniverseTime = await _universeService.GetUniverseCurrentTime();
                _logger.LogInformation($"CURRENT UNIVERSE TIME: {currentUniverseTime.CurrentTime}");

                return await _queries.GetCitizens(query.PageIndex, query.PageSize);
            }
        }
    }
}