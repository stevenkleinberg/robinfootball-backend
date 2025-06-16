using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RobinFootball.API;
using RobinFootball.API.Models;
using RobinFootball.API.Data;
using Xunit;

public class PlayerApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PlayerApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove real DB
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    // Add in-memory DB
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });

                    // Seed data
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.EnsureCreated();
                    db.Players.Add(new Player
                    {
                        Name = "Test Player",
                        Position = "QB",
                        Team = "TEST",
                        InitialValue = 100,
                        CurrentValue = 100
                    });
                    db.SaveChanges();
                });
            })
            .CreateClient();
    }

    [Fact]
    public async Task GetPlayers_ReturnsSeededPlayer()
    {
        var players = await _client.GetFromJsonAsync<List<Player>>("/api/Players");
        Assert.NotNull(players);
        Assert.Single(players);
        Assert.Equal("Test Player", players[0].Name);
    }
}
