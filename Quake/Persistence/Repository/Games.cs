using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Persistence.Database;
using System;
using System.Collections.Generic;

namespace Quake.Persistence.Repository
{
    public class Games : IGames, IDisposable
    {
        private readonly QuakeContext context;

        public Games(QuakeContext context)
        {
            this.context = context ?? throw new ArgumentException("The connection to the database was not reported.");
        }

        public void Save(List<Game> games)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Game.AddRange(games);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            context?.Dispose();
        }
    }
}
