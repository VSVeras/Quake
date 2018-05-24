namespace Quake.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeadPlayer",
                c => new
                    {
                        IdGame = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 30, unicode: false),
                        TotalKills = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdGame, t.Id })
                .ForeignKey("dbo.Game", t => t.IdGame, cascadeDelete: true)
                .Index(t => t.IdGame);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalKills = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KillsByMeans",
                c => new
                    {
                        IdGame = c.Int(nullable: false),
                        MeansOfDeath = c.Int(nullable: false),
                        TotalKills = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdGame, t.MeansOfDeath })
                .ForeignKey("dbo.Game", t => t.IdGame, cascadeDelete: true)
                .Index(t => t.IdGame);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        IdGame = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => new { t.IdGame, t.Id })
                .ForeignKey("dbo.Game", t => t.IdGame, cascadeDelete: true)
                .Index(t => t.IdGame);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "IdGame", "dbo.Game");
            DropForeignKey("dbo.KillsByMeans", "IdGame", "dbo.Game");
            DropForeignKey("dbo.DeadPlayer", "IdGame", "dbo.Game");
            DropIndex("dbo.Player", new[] { "IdGame" });
            DropIndex("dbo.KillsByMeans", new[] { "IdGame" });
            DropIndex("dbo.DeadPlayer", new[] { "IdGame" });
            DropTable("dbo.Player");
            DropTable("dbo.KillsByMeans");
            DropTable("dbo.Game");
            DropTable("dbo.DeadPlayer");
        }
    }
}
