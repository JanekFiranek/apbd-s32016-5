namespace Apbd5.Models;

public class ComponentManufacturer
{
    public int Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string FullName { get; init; }
    public DateOnly FoundationDate { get; init; }

    public ICollection<Component> Components { get; init; } = [];
}