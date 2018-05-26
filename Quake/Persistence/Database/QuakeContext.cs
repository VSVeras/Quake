namespace Quake.Persistence.Database
{
    using Quake.Entities;
    using System;
    using System.Data.Entity;

    internal sealed class QuakeContext : DbContext, IDisposable
    {
        public QuakeContext() : base("name=QuakeContext")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<DeadPlayer> DeadPlayer { get; set; }
        public DbSet<KillsByMeans> KillsByMeans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MappingGame(modelBuilder);
            MappingPlayes(modelBuilder);
            MappingDeadPlayer(modelBuilder);
            MappingKillsByMeans(modelBuilder);
        }

        private void MappingGame(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Game>().Property(e => e.TotalKills).HasPrecision(18, 2);
            modelBuilder.Entity<Game>().HasMany(e => e.Players).WithRequired(e => e.Game).HasForeignKey(e => e.IdGame).WillCascadeOnDelete(true);
            modelBuilder.Entity<Game>().HasMany(e => e.DeadPlayers).WithRequired(e => e.Game).HasForeignKey(e => e.IdGame).WillCascadeOnDelete(true);
            modelBuilder.Entity<Game>().HasMany(e => e.KillsByMeans).WithRequired(e => e.Game).HasForeignKey(e => e.IdGame).WillCascadeOnDelete(true);
        }

        private void MappingPlayes(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Player>().HasKey(e => new { e.IdGame, e.Id });
            modelBuilder.Entity<Player>().Property(e => e.Name).HasMaxLength(30).IsUnicode(false);
        }

        private void MappingDeadPlayer(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeadPlayer>().ToTable("DeadPlayer");
            modelBuilder.Entity<DeadPlayer>().HasKey(e => new { e.IdGame, e.Id });
            modelBuilder.Entity<DeadPlayer>().Property(e => e.Name).HasMaxLength(30).IsUnicode(false);
            modelBuilder.Entity<DeadPlayer>().Property(e => e.TotalKills).HasPrecision(18, 2);
        }

        private void MappingKillsByMeans(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KillsByMeans>().ToTable("KillsByMeans");
            modelBuilder.Entity<KillsByMeans>().HasKey(e => new { e.IdGame, e.MeansOfDeath });
            modelBuilder.Entity<KillsByMeans>().Property(e => e.TotalKills).HasPrecision(18, 2);
        }
    }
}
