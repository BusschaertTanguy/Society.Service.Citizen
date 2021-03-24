using System;
using System.Threading.Tasks;
using Application.ReadModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public static class GetCitizen
    {
        public class Query : IRequest<CitizenDetailReadModel>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        internal class Handler : QueryHandler<Query, CitizenDetailReadModel>
        {
            private readonly ICitizenQueries _queries;

            public Handler(ILogger<QueryHandler<Query, CitizenDetailReadModel>> logger, ICitizenQueries queries) : base(logger)
            {
                _queries = queries;
            }

            protected override Task<CitizenDetailReadModel> Process(Query query)
            {
                return _queries.GetCitizen(query.Id);
            }
        }
    }
}