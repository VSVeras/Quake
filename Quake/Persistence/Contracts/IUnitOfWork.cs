using System.Data.Entity;

namespace Quake.Persistence.Contracts
{
    public interface IUnitOfWork
    {
        DbContext Current();
    }
}
