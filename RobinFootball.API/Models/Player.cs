namespace RobinFootball.API.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string Team { get; set; } = default!;
    public decimal InitialValue { get; set; }
    public decimal CurrentValue { get; set; }
}
