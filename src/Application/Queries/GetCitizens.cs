using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Connections;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public static class GetCitizens
    {
        public class Query : IRequest<IEnumerable<Result>>
        {
            public Query(int? pageIndex = 0, int? pageSize = 25)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
            }

            public int? PageIndex { get; }
            public int? PageSize { get; }
        }

        public class Result
        {
            public Result(Guid id, string name)
            {
                Id = id;
                Name = name;
            }

            public Guid Id { get; }
            public string Name { get; }
        }

        internal class Handler : QueryHandler<Query, IEnumerable<Result>>
        {
            private readonly IDbConnectionProvider _connectionProvider;

            public Handler(ILogger<QueryHandler<Query, IEnumerable<Result>>> logger, IDbConnectionProvider connectionProvider) : base(logger)
            {
                _connectionProvider = connectionProvider;
            }

            protected override async Task<IEnumerable<Result>> Process(Query query)
            {
                using var connection = _connectionProvider.GetDbConnection();

                const string dbQuery = @"SELECT [Id], [Name] FROM Citizen ORDER BY Id OFFSET (@PageIndex * @PageSize) ROWS  FETCH NEXT @PageSize ROWS ONLY;";

                return await connection.QueryAsync<Result>(dbQuery, new
                {
                    query.PageIndex,
                    query.PageSize
                });
            }
        }
    }
}