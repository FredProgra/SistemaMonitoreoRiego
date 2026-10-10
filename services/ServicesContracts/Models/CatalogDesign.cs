namespace Ms06ServicesContracts.Models;

public class CatalogDesign
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public decimal ReferencePrice { get; set; }
    public bool IsActive { get; set; } = true;

    public bool EstaDisponible() => IsActive;
}