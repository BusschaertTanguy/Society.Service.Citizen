using System.Threading.Tasks;
using Application.Transactions;
using Infrastructure.EntityFramework.Contexts;

namespace Infrastructure.EntityFramework.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CitizenDbContext _context;

        public UnitOfWork(CitizenDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}