namespace Ms06ServicesContracts.DTOs;

public record ContractUploadRequestDTO(string CustomerName, string ProjectName, string ContractType,
    string FileName, string FileUrl);

public record ContractResponseDTO(Guid Id, string CustomerName, string ProjectName, string ContractType,
    string FileName, string FileUrl, DateTime UploadedAt);