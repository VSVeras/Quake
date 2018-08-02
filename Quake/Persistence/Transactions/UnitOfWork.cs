using Quake.Persistence.Contracts;
using Quake.Persistence.Database;
using System;
using System.Data.Entity;

namespace Quake.Persistence.Transactions
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _uow;

        public UnitOfWork(DbContext uow)
        {
            _uow = uow;
        }

        public DbContext Current()
        {
            return _uow;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            _uow?.Dispose();
        }
    }
}
