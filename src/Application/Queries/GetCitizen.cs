using System;
using System.Threading.Tasks;
using Application.Connections;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

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
            public Result(Guid id, Gender gender, string name, DateTime dateOfBirth)
            {
                Id = id;
                Gender = gender;
                Name = name;
                DateOfBirth = dateOfBirth;
            }

            public DateTime DateOfBirth { get; }
            public Gender Gender { get; }
            public Guid Id { get; }
            public string Name { get; }
        }

        internal class Handler : QueryHandler<Query, Result>
        {
            private readonly IDbConnectionProvider _connectionProvider;

            public Handler(ILogger<QueryHandler<Query, Result>> logger, IDbConnectionProvider connectionProvider) : base(logger)
            {
                _connectionProvider = connectionProvider;
            }

            protected override async Task<Result> Process(Query query)
            {
                using var connection = _connectionProvider.GetDbConnection();

                const string dbQuery = "SELECT [Id], [Gender], [Name], [DateOfBirth] FROM [Citizen] WHERE Id = @Id;";

                return await connection.QueryFirstOrDefaultAsync<Result>(dbQuery, new
                {
                    query.Id
                });
            }
        }
    }
}