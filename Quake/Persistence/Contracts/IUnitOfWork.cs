using System;
using System.Data.Entity;

namespace Quake.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Current();
    }
}
