using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ReadModels;
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
            private readonly ICitizenQueries _queries;

            public Handler(ILogger<QueryHandler<Query, IEnumerable<CitizenReadModel>>> logger, ICitizenQueries queries) : base(logger)
            {
                _queries = queries;
            }

            protected override Task<IEnumerable<CitizenReadModel>> Process(Query query)
            {
                return _queries.GetCitizens(query.PageIndex, query.PageSize);
            }
        }
    }
}