using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms06ServicesContracts.DTOs;
using Ms06ServicesContracts.Services;

namespace Ms06ServicesContracts.Controllers;

[ApiController]
[Route("api/v1/contracts")]
[Authorize]
public class ContractController : ControllerBase
{
    private readonly IContractService _service;
    public ContractController(IContractService service) => _service = service;

    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<ActionResult<ContractResponseDTO>> Upload([FromBody] ContractUploadRequestDTO request)
    {
        var result = await _service.UploadAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "ADMIN,MANAGER")]
    public async Task<ActionResult<ContractResponseDTO>> GetById(Guid id) =>
        Ok(await _service.GetByIdAsync(id));

    [HttpGet("catalog-designs")]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<List<CatalogDesignResponseDTO>>> GetCatalog([FromQuery] string? category) =>
        Ok(await _service.GetCatalogAsync(category));

    [HttpGet("catalog-designs/{id:guid}")]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<CatalogDesignResponseDTO>> GetCatalogDesignById(Guid id) =>
        Ok(await _service.GetCatalogDesignByIdAsync(id));
}