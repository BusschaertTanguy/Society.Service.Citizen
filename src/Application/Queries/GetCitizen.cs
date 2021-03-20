using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Connections;
using Dapper;
using MediatR;

namespace Application.Queries
{
    public static class GetCitizen
    {
        public class Query : IRequest<Result>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
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

        internal class Handler : IRequestHandler<Query, Result>
        {
            private readonly IDbConnectionProvider _connectionProvider;

            public Handler(IDbConnectionProvider connectionProvider)
            {
                _connectionProvider = connectionProvider;
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                using var connection = _connectionProvider.GetDbConnection();
                
                const string dbQuery = "SELECT [Id], [Name] FROM [Citizen] WHERE Id = @Id;";
                
                return await connection.QueryFirstOrDefaultAsync<Result>(dbQuery, new
                {
                    query.Id
                });
            }
        }
    }
}