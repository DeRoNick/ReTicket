using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Database;

public class ReTicketDbContext : IdentityDbContext
{
    public ReTicketDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketListing> TicketListings { get; set; }
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ReTicketDbContext).Assembly);
    }
}