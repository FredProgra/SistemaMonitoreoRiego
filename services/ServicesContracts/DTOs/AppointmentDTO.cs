namespace Ms06ServicesContracts.DTOs;

public record CreateAppointmentRequestDTO(string CustomerName, string CustomerPhone, string Address,
    string ServiceType, DateTime ScheduledAt, string? Notes);

public record AppointmentResponseDTO(Guid Id, string CustomerName, string CustomerPhone, string Address,
    string ServiceType, DateTime ScheduledAt, string Status, string? Notes);