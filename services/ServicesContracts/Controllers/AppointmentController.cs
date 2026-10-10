using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ms06ServicesContracts.DTOs;
using Ms06ServicesContracts.Services;

namespace Ms06ServicesContracts.Controllers;

[ApiController]
[Route("api/v1/appointments")]
[Authorize]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;
    public AppointmentController(IAppointmentService service) => _service = service;

    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<AppointmentResponseDTO>> Create([FromBody] CreateAppointmentRequestDTO request)
    {
        var result = await _service.CreateAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<AppointmentResponseDTO>> GetById(Guid id) =>
        Ok(await _service.GetByIdAsync(id));

    [HttpGet]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<List<AppointmentResponseDTO>>> GetByStatus([FromQuery] string status = "PENDING") =>
        Ok(await _service.GetByStatusAsync(status));

    [HttpPatch("{id:guid}/cancel")]
    [Authorize(Roles = "OPERATOR,EMPLOYEE,ADMIN,MANAGER")]
    public async Task<ActionResult<AppointmentResponseDTO>> Cancel(Guid id) =>
        Ok(await _service.CancelAsync(id));
}