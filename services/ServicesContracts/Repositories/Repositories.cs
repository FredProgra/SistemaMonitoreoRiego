using Microsoft.EntityFrameworkCore;
using Ms06ServicesContracts.Data;
using Ms06ServicesContracts.Models;

namespace Ms06ServicesContracts.Repositories;

public interface IAppointmentRepository
{
    Task<ServiceAppointment?> FindByIdAsync(Guid id);
    Task<List<ServiceAppointment>> FindByStatusAsync(string status);
    Task<bool> ExistsByAddressAndScheduledAtAsync(string address, DateTime scheduledAt);
    Task AddAsync(ServiceAppointment entity);
    Task SaveChangesAsync();
}

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ServicesContractsDbContext _db;
    public AppointmentRepository(ServicesContractsDbContext db) => _db = db;

    public Task<ServiceAppointment?> FindByIdAsync(Guid id) =>
        _db.ServiceAppointments.FirstOrDefaultAsync(a => a.Id == id);
    public Task<List<ServiceAppointment>> FindByStatusAsync(string status) =>
        _db.ServiceAppointments.Where(a => a.Status == status).OrderBy(a => a.ScheduledAt).ToListAsync();
    public Task<bool> ExistsByAddressAndScheduledAtAsync(string address, DateTime scheduledAt) =>
        _db.ServiceAppointments.AnyAsync(a => a.Address == address && a.ScheduledAt == scheduledAt
                                              && a.Status != "CANCELLED");
    public async Task AddAsync(ServiceAppointment entity) => await _db.ServiceAppointments.AddAsync(entity);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}

public interface IContractRepository
{
    Task<ProjectContract?> FindByIdAsync(Guid id);
    Task<List<ProjectContract>> FindByContractTypeAsync(string contractType);
    Task AddAsync(ProjectContract entity);
    Task SaveChangesAsync();
}

public class ContractRepository : IContractRepository
{
    private readonly ServicesContractsDbContext _db;
    public ContractRepository(ServicesContractsDbContext db) => _db = db;

    public Task<ProjectContract?> FindByIdAsync(Guid id) =>
        _db.ProjectContracts.FirstOrDefaultAsync(c => c.Id == id);
    public Task<List<ProjectContract>> FindByContractTypeAsync(string contractType) =>
        _db.ProjectContracts.Where(c => c.ContractType == contractType).ToListAsync();
    public async Task AddAsync(ProjectContract entity) => await _db.ProjectContracts.AddAsync(entity);
    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}

public interface ICatalogDesignRepository
{
    Task<CatalogDesign?> FindByIdAsync(Guid id);
    Task<List<CatalogDesign>> FindByIsActiveAsync(bool isActive);
    Task<List<CatalogDesign>> FindByCategoryAndIsActiveAsync(string category, bool isActive);
}

public class CatalogDesignRepository : ICatalogDesignRepository
{
    private readonly ServicesContractsDbContext _db;
    public CatalogDesignRepository(ServicesContractsDbContext db) => _db = db;

    public Task<CatalogDesign?> FindByIdAsync(Guid id) =>
        _db.CatalogDesigns.FirstOrDefaultAsync(d => d.Id == id);
    public Task<List<CatalogDesign>> FindByIsActiveAsync(bool isActive) =>
        _db.CatalogDesigns.Where(d => d.IsActive == isActive).ToListAsync();
    public Task<List<CatalogDesign>> FindByCategoryAndIsActiveAsync(string category, bool isActive) =>
        _db.CatalogDesigns.Where(d => d.Category == category && d.IsActive == isActive).ToListAsync();
}