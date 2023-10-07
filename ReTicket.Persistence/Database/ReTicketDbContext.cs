using Microsoft.EntityFrameworkCore;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Database;

public class ReTicketDbContext : DbContext
{
    public ReTicketDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketListing> TicketListings { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReTicketDbContext).Assembly);
    }
}