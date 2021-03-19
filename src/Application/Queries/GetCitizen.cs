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
        public class Request : IRequest<Response>
        {
            public Request(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public class Response
        {
            public Response(Guid id, string name)
            {
                Id = id;
                Name = name;
            }

            public Guid Id { get; }
            public string Name { get; }
        }

        internal class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDbConnectionProvider _connectionProvider;

            public Handler(IDbConnectionProvider connectionProvider)
            {
                _connectionProvider = connectionProvider;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                using var connection = _connectionProvider.GetDbConnection();
                
                const string query = "SELECT [Id], [Name] FROM [Citizen] WHERE Id = @Id;";
                
                return await connection.QueryFirstOrDefaultAsync<Response>(query, new
                {
                    request.Id
                });
            }
        }
    }
}