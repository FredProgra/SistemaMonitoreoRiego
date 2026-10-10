namespace Ms06ServicesContracts.Models;

public class ProjectContract
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string ContractType { get; set; } = string.Empty; // CONSTRUCTION, DESIGN
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
    public Guid UploadedByUserId { get; set; }

    public bool TieneArchivo() => !string.IsNullOrWhiteSpace(FileUrl);
}