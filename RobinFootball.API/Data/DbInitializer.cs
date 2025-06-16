using RobinFootball.API.Models;

namespace RobinFootball.API.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Ensure DB is created
        context.Database.EnsureCreated();

        // Exit if data already exists
        if (context.Players.Any()) return;

        var players = new List<Player>
        {
            new Player { Name = "Patrick Mahomes", Position = "QB", Team = "KC", InitialValue = 150, CurrentValue = 150 },
            new Player { Name = "Ja'Marr Chase", Position = "WR", Team = "CIN", InitialValue = 135, CurrentValue = 135 },
            new Player { Name = "Christian McCaffrey", Position = "RB", Team = "SF", InitialValue = 145, CurrentValue = 145 },
            new Player { Name = "CeeDee Lamb", Position = "WR", Team = "DAL", InitialValue = 130, CurrentValue = 130 }
        };

        context.Players.AddRange(players);
        context.SaveChanges();
    }
}
