using Quake.Entities;
using Quake.Entities.Contracts;
using Quake.Persistence.Database;
using System;
using System.Collections.Generic;

namespace Quake.Persistence.Repository
{
    public class Games : IGames
    {
        private readonly QuakeContext Context;

        public Games(QuakeContext context)
        {
            Context = context ?? throw new ArgumentException("The connection to the database was not reported.");
        }

        public void Save(List<Game> games)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.Game.AddRange(games);
                    Context.SaveChanges();
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
