using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Persistence.Contracts;
using System.Collections.Generic;

namespace Quake.Persistence.Repository
{
    public class Games : IGames
    {
        private readonly IUnitOfWork _uow;

        public Games(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Save(List<Game> games)
        {
            using (var transaction = _uow.Current().Database.BeginTransaction())
            {
                try
                {
                    _uow.Current().Set<Game>().AddRange(games);
                    _uow.Current().SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}