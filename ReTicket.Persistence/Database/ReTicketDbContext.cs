using Microsoft.EntityFrameworkCore;

namespace ReTicket.Persistence.Database;

public class ReTicketDbContext : DbContext
{
    public ReTicketDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReTicketDbContext).Assembly);
    }
}