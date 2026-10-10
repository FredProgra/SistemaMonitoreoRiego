using Ms06ServicesContracts.DTOs;
using Ms06ServicesContracts.Models;

namespace Ms06ServicesContracts.Mappers;

public static class MapperExtensions
{
    public static AppointmentResponseDTO ToDto(this ServiceAppointment e) =>
        new(e.Id, e.CustomerName, e.CustomerPhone, e.Address, e.ServiceType, e.ScheduledAt, e.Status, e.Notes);

    public static ContractResponseDTO ToDto(this ProjectContract e) =>
        new(e.Id, e.CustomerName, e.ProjectName, e.ContractType, e.FileName, e.FileUrl, e.UploadedAt);

    public static CatalogDesignResponseDTO ToDto(this CatalogDesign e) =>
        new(e.Id, e.Name, e.Category, e.Description, e.ImageUrl, e.VideoUrl, e.ReferencePrice);
}