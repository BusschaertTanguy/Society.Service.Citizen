using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ReadModels;

namespace Application.Queries
{
    internal interface ICitizenQueries
    {
        Task<CitizenDetailReadModel> GetCitizen(Guid id);
        Task<IEnumerable<CitizenReadModel>> GetCitizens(int pageIndex, int pageSize);
    }
}