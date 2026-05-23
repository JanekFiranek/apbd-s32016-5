namespace Apbd5.Models;

public class PCComponent
{
    public int PCId { get; init; }
    public required string ComponentCode { get; init; }
    public int Amount { get; init; }

    public PC PC { get; init; } = null!;
    public Component Component { get; init; } = null!;
}