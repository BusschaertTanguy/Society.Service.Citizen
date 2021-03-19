using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    internal class CitizenRepository : ICitizenRepository
    {
        private readonly CitizenDbContext _context;

        public CitizenRepository(CitizenDbContext context)
        {
            _context = context;
        }

        public async Task<Citizen> GetById(Guid id)
        {
            return await _context.Set<Citizen>().FirstOrDefaultAsync(citizen => citizen.Id == id);
        }

        public async Task Add(Citizen citizen)
        {
            await _context.Set<Citizen>().AddAsync(citizen);
        }
    }
}