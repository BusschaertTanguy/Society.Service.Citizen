using System.Data;

namespace Application.Connections
{
    internal interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}