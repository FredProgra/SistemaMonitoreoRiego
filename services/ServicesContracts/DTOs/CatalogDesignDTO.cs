namespace Ms06ServicesContracts.DTOs;

public record CatalogDesignResponseDTO(Guid Id, string Name, string Category, string Description,
    string ImageUrl, string? VideoUrl, decimal ReferencePrice);