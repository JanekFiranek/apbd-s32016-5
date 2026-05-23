namespace Apbd5.Models;

public class ComponentType
{
    public int Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string Name { get; init; }

    public ICollection<Component> Components { get; init; } = [];
}