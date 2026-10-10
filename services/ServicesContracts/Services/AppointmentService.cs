using Ms06ServicesContracts.DTOs;
using Ms06ServicesContracts.Exceptions;
using Ms06ServicesContracts.Mappers;
using Ms06ServicesContracts.Models;
using Ms06ServicesContracts.Repositories;

namespace Ms06ServicesContracts.Services;

public interface IAppointmentService
{
    Task<AppointmentResponseDTO> CreateAsync(Guid userId, CreateAppointmentRequestDTO request);
    Task<AppointmentResponseDTO> GetByIdAsync(Guid id);
    Task<List<AppointmentResponseDTO>> GetByStatusAsync(string status);
    Task<AppointmentResponseDTO> CancelAsync(Guid id);
}

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    public AppointmentService(IAppointmentRepository repo) => _repo = repo;

    public async Task<AppointmentResponseDTO> CreateAsync(Guid userId, CreateAppointmentRequestDTO request)
    {
        if (request.ScheduledAt <= DateTime.UtcNow)
            throw new BusinessRuleException("El servicio debe agendarse en una fecha futura.");
        if (await _repo.ExistsByAddressAndScheduledAtAsync(request.Address, request.ScheduledAt))
            throw new BusinessRuleException("Ya existe un servicio agendado para ese domicilio y horario.");

        var entity = new ServiceAppointment
        {
            Id = Guid.NewGuid(),
            CustomerName = request.CustomerName,
            CustomerPhone = request.CustomerPhone,
            Address = request.Address,
            ServiceType = request.ServiceType,
            ScheduledAt = request.ScheduledAt,
            Notes = request.Notes,
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity.ToDto();
    }

    public async Task<AppointmentResponseDTO> GetByIdAsync(Guid id) => (await FindOrThrow(id)).ToDto();

    public async Task<List<AppointmentResponseDTO>> GetByStatusAsync(string status) =>
        (await _repo.FindByStatusAsync(status)).Select(a => a.ToDto()).ToList();

    public async Task<AppointmentResponseDTO> CancelAsync(Guid id)
    {
        var entity = await FindOrThrow(id);
        if (!entity.EstaPendiente())
            throw new BusinessRuleException("Solo se pueden cancelar servicios pendientes.");
        entity.Status = "CANCELLED";
        await _repo.SaveChangesAsync();
        return entity.ToDto();
    }

    private async Task<ServiceAppointment> FindOrThrow(Guid id) =>
        await _repo.FindByIdAsync(id)
        ?? throw new ResourceNotFoundException($"Servicio {id} no encontrado.");
}