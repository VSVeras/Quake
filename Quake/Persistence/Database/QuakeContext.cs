namespace Quake.Persistence.Database
{
    using Quake.Entities;
    using System.Data.Entity;

    public class QuakeContext : DbContext
    {
        public QuakeContext() : base("name=QuakeContext")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<DeadPlayer> DeadPlayers { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MappingPlayes(modelBuilder);

            MappingDeadPlayer(modelBuilder);

            MappingGame(modelBuilder);
        }

        private void MappingGame(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Game>().Property(e => e.TotalKills).HasPrecision(18, 2);
            modelBuilder.Entity<Game>().HasMany(e => e.Players).WithRequired(e => e.Game).HasForeignKey(e => e.IdGame).WillCascadeOnDelete(true);
            modelBuilder.Entity<Game>().HasMany(e => e.DeadPlayers).WithRequired(e => e.Game).HasForeignKey(e => e.IdGame).WillCascadeOnDelete(true);
        }

        private void MappingDeadPlayer(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeadPlayer>().ToTable("DeadPlayer");
            modelBuilder.Entity<DeadPlayer>().HasKey(e => new { e.IdGame, e.Id });
            modelBuilder.Entity<DeadPlayer>().Property(e => e.Name).HasMaxLength(30).IsUnicode(false);
            modelBuilder.Entity<DeadPlayer>().Property(e => e.TotalKills).HasPrecision(18, 2);
        }

        private void MappingPlayes(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Player>().HasKey(e => new { e.IdGame, e.Id });
            modelBuilder.Entity<Player>().Property(e => e.Name).HasMaxLength(30).IsUnicode(false);
        }
    }
}
