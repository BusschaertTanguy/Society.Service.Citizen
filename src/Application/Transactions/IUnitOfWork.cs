using System.Threading.Tasks;

namespace Application.Transactions
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}