using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICitizenRepository
    {
        Task<Citizen> GetById(Guid id);
        Task Add(Citizen citizen);
    }
}