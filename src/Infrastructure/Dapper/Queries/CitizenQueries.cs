using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries;
using Application.ReadModels;
using Dapper;
using Infrastructure.Queries;

namespace Infrastructure.Dapper.Queries
{
    internal class CitizenQueries : ICitizenQueries
    {
        private readonly SqlDbConnectionProvider _connectionProvider;

        public CitizenQueries(SqlDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<CitizenDetailReadModel> GetCitizen(Guid id)
        {
            using var connection = _connectionProvider.GetDbConnection();

            const string dbQuery = "SELECT [Id], [Gender], [Name], [DateOfBirth] FROM [Citizen] WHERE Id = @id;";

            return await connection.QueryFirstOrDefaultAsync<CitizenDetailReadModel>(dbQuery, new
            {
                id
            });
        }

        public async Task<IEnumerable<CitizenReadModel>> GetCitizens(int pageIndex, int pageSize)
        {
            using var connection = _connectionProvider.GetDbConnection();

            const string dbQuery = @"SELECT [Id], [Name] FROM Citizen ORDER BY Id OFFSET (@pageIndex * @pageSize) ROWS  FETCH NEXT @pageSize ROWS ONLY;";

            return await connection.QueryAsync<CitizenReadModel>(dbQuery, new
            {
                pageIndex,
                pageSize
            });
        }
    }
}