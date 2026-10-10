using Ms06ServicesContracts.DTOs;
using Ms06ServicesContracts.Exceptions;
using Ms06ServicesContracts.Mappers;
using Ms06ServicesContracts.Models;
using Ms06ServicesContracts.Repositories;

namespace Ms06ServicesContracts.Services;

public interface IContractService
{
    Task<ContractResponseDTO> UploadAsync(Guid userId, ContractUploadRequestDTO request);
    Task<ContractResponseDTO> GetByIdAsync(Guid id);
    Task<List<CatalogDesignResponseDTO>> GetCatalogAsync(string? category);
    Task<CatalogDesignResponseDTO> GetCatalogDesignByIdAsync(Guid id);
}

public class ContractService : IContractService
{
    private static readonly string[] ValidTypes = { "CONSTRUCTION", "DESIGN" };

    private readonly IContractRepository _contracts;
    private readonly ICatalogDesignRepository _designs;

    public ContractService(IContractRepository contracts, ICatalogDesignRepository designs)
    {
        _contracts = contracts;
        _designs = designs;
    }

    public async Task<ContractResponseDTO> UploadAsync(Guid userId, ContractUploadRequestDTO request)
    {
        if (!ValidTypes.Contains(request.ContractType))
            throw new BusinessRuleException("El tipo de contrato debe ser CONSTRUCTION o DESIGN.");

        var entity = new ProjectContract
        {
            Id = Guid.NewGuid(),
            CustomerName = request.CustomerName,
            ProjectName = request.ProjectName,
            ContractType = request.ContractType,
            FileName = request.FileName,
            FileUrl = request.FileUrl,
            UploadedAt = DateTime.UtcNow,
            UploadedByUserId = userId
        };
        if (!entity.TieneArchivo())
            throw new BusinessRuleException("El contrato digitalizado debe incluir un archivo.");

        await _contracts.AddAsync(entity);
        await _contracts.SaveChangesAsync();
        return entity.ToDto();
    }

    public async Task<ContractResponseDTO> GetByIdAsync(Guid id) =>
        (await _contracts.FindByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Contrato {id} no encontrado.")).ToDto();

    public async Task<List<CatalogDesignResponseDTO>> GetCatalogAsync(string? category)
    {
        var list = string.IsNullOrWhiteSpace(category)
            ? await _designs.FindByIsActiveAsync(true)
            : await _designs.FindByCategoryAndIsActiveAsync(category, true);
        return list.Select(d => d.ToDto()).ToList();
    }

    public async Task<CatalogDesignResponseDTO> GetCatalogDesignByIdAsync(Guid id) =>
        (await _designs.FindByIdAsync(id)
            ?? throw new ResourceNotFoundException($"Diseño {id} no encontrado.")).ToDto();
}