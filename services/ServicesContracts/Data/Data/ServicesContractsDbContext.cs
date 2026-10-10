using Microsoft.EntityFrameworkCore;
using Ms06ServicesContracts.Models;

namespace Ms06ServicesContracts.Data;

public class ServicesContractsDbContext : DbContext
{
    public ServicesContractsDbContext(DbContextOptions<ServicesContractsDbContext> options) : base(options) { }

    public DbSet<ServiceAppointment> ServiceAppointments => Set<ServiceAppointment>();
    public DbSet<ProjectContract> ProjectContracts => Set<ProjectContract>();
    public DbSet<CatalogDesign> CatalogDesigns => Set<CatalogDesign>();
}