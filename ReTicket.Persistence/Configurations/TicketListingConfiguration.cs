using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTicket.Domain.Models;

namespace ReTicket.Persistence.Configurations
{
    public class TicketListingConfiguration : IEntityTypeConfiguration<TicketListing>
    {
        public void Configure(EntityTypeBuilder<TicketListing> builder)
        {
            builder.ToTable("TicketListings", "dbo");

            _ = builder.HasKey(x => x.Id);

            _ = builder.HasOne<Ticket>(s => s.Ticket)
                .WithMany(g => g.TicketListings)
                .HasForeignKey(s => s.TicketId).OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasOne<AppUser>(s => s.User)
                .WithMany(g => g.ListedTickets)
                .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.NoAction);

            _ = builder.HasOne<Event>(s => s.Event)
                .WithMany(g => g.TicketListings)
                .HasForeignKey(s => s.EventId).OnDelete(DeleteBehavior.NoAction);

            _ = builder.Property(x => x.Price).HasColumnType("Money");
        }
    }
}
